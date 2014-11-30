
open Printf

open Lwt
open Cohttp
open Cohttp_lwt_unix


type human = {name: string; age: int}

let human_to_json h =
  let j = Ezjsonm.(`O [("name", `String h.name);
                       ("age", `Float (float_of_int h.age))]) in
  Ezjsonm.to_string j
let human_from_json s =
  try
    let jo = Ezjsonm.from_string s in
    let jname = Ezjsonm.(get_string @@ find jo ["name"]) in
    let jage = Ezjsonm.(get_int @@ find jo ["age"]) in
    Some({name=jname; age=jage})
  with
  | Not_found -> None
  | Ezjsonm.Parse_error(_) -> None


module HumanOrd : AVLTree.Ord with type t = human =
  struct
    type t = human
    let compare h1 h2 =
      AVLTree.(
        if ((h1.age = h2.age) && (h1.name = h2.name)) then Eq
        else if h1.age > h2.age then Gt
        else Lt)
  end
module HumanTree = AVLTree.Make(HumanOrd)


let swap var func =
  let res = func !var in
  let () = var := res in
  res


let not_allowed = Server.respond_error
                    ~status:`Method_not_allowed
                    ~body:"Method not allowed\n" ()
let unpocessable_entity = Server.respond_error
                            ~status:`Unprocessable_entity
                            ~body:"Method not allowed\n" ()
let unsupported_media_type ctype = Server.respond_error
                                     ~status:`Unsupported_media_type
                                     ~body:(sprintf "Unsupported content-type: %s\n" ctype) ()
let not_found path =  Server.respond_error
                        ~status:`Not_found
                        ~body:(sprintf "Not found '%s'\n" path) ()

let respond_json json =
  let size = String.length json in
  let headers = Header.init_with "Content-Length" (string_of_int size) in
  let headers = Header.add headers "Content-Type" "application/json" in
  let encoding = Transfer.Fixed (Int64.of_int size) in
  let res = Server.Response.make ~status:`OK ~encoding ~headers () in
  return (res, Cohttp_lwt_body.of_string json)


let get_people data uri =
  let uname = Uri.get_query_param uri "name" in
  let uage = Uri.get_query_param uri "age" in
  match (uname, uage) with
  | (Some(n), Some(a)) ->
     let look_for_human = {name=n; age=(int_of_string a)} in
     let res = HumanTree.lookup !data look_for_human in
     (match res with
      | Some(r) -> respond_json @@ human_to_json r
      | None -> not_found @@ Uri.to_string uri)
  | _ -> not_found @@ Uri.to_string uri


let update_people data body =
  Cohttp_lwt_body.to_string body >>=
    (fun body ->
     let jh = human_from_json body in
     match jh with
     | Some(h) ->
        let t_ = swap data (fun d -> HumanTree.insert d h) in
        respond_json "{\"status\": \"ok\"}"
     | None -> unpocessable_entity)


let handler data (ch, conn) req body =
  let uri = Request.uri req  in
  let path = Uri.path uri in
  let meth = Request.meth req in
  let smeth = Code.string_of_method meth in
  let content_type = Header.get (Request.headers req) "content-type" in
  let () = printf "Got %s request to %s\n" smeth path in
  match path with
  | "/people" -> (match meth with
                 | `GET -> get_people data uri
                 | `POST -> (match content_type with
                             | Some("application/json") -> update_people data body
                             | Some(ctype) -> unsupported_media_type ctype
                             | None -> unsupported_media_type "unknown")
                 | _ -> not_allowed)
  | p -> not_found p


let start_server port host =
  let () = printf "Listening for HTTP request on: %s %d\n" host port in
  let data = ref @@ HumanTree.empty () in
  let conn_closed (ch,conn) () = () in
  let callback = handler data in
  let config = { Server.callback; conn_closed } in
    Server.create ~mode:(`TCP (`Port port)) config


let () =
  Lwt_unix.run (start_server 31337 "127.0.0.1" )
