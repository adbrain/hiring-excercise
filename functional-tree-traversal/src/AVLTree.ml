

type order = Lt | Eq | Gt
module type Ord =
sig
  type t
  val compare : t -> t -> order
end

module type AVLTree =
sig
  type elem
  type tree

  val empty : unit -> tree
  val is_empty : tree -> bool
  val insert : tree -> elem -> tree
  val member : tree -> elem -> bool
  val lookup : tree -> elem -> elem option
end

module Make (Elem : Ord) =
struct
  type elem = Elem.t
  type tree = Leaf | Node of int * tree * elem * tree

  let empty () = Leaf

  let is_empty tree = tree = Leaf

  let rec member t e =
    match t with
    | Node (_, l, x, r) ->
       (match Elem.compare e x with
        | Lt -> member l e
        | Eq -> true
        | Gt -> member r e)
    | Leaf -> false

  let rec lookup t e =
    match t with
    | Node (_, l, x, r) ->
       (match Elem.compare e x with
        | Lt -> lookup l e
        | Eq -> Some(x)
        | Gt -> lookup r e)
    | Leaf -> None

  let depth tree =
    match tree with
    | Node (d, _, _, _) -> d
    | Leaf -> 0

  let value tree =
    match tree with
    | Node (_, _, x, _) -> x
    | Leaf -> failwith "Impossible"

  let balanceLL node =
    match node with
    | (Node (d, Node (lmax, ll, xl, rl), x, r)) ->
       let rmax = max (depth rl) (depth r) + 1 in
       let cmax = max rmax (depth ll) + 1 in
       Node (cmax, ll, xl, Node (rmax, rl, x, r))
    | _ -> failwith "Impossible"

  let balanceLR node =
    match node with
    | (Node (d, Node (dl, ll, y, Node (dlr, lrl, z, lrr)), x, r)) ->
       let lmax = max (depth ll) (depth lrl) + 1 in
       let rmax = max (depth lrr) (depth r) + 1 in
       let cmax = max lmax rmax + 1 in
       Node (cmax, Node (lmax, ll, y, lrl), z, Node (rmax, lrr, x, r))
    | _ -> failwith "Impossible"

  let balanceRR node = match node with
    | (Node (d, l, x, Node (dr, lr, xr, rr))) ->
       let lmax = max (depth l) (depth lr) + 1 in
       let cmax = max lmax (depth rr) + 1 in
       Node (cmax, Node (lmax, l, x, lr), xr, rr)
    | _ -> failwith "Impossible"

  let balanceRL node = match node with
    | (Node (d, l, x, Node (dr, Node (drl, rll, z, rlr), y, rr))) ->
       let lmax = max (depth l) (depth rll) + 1 in
       let rmax = max (depth rlr) (depth rr) + 1 in
       let cmax = max lmax rmax + 1 in
       Node (cmax, Node (lmax, l, x, rll), z, Node (rmax, rlr, y, rr))
    | _ -> failwith "Impossible"

  let rec insert t e =
    match t with
    | Node (_, l, x, r) ->
       (match Elem.compare e x with
          Lt ->
          let insL = insert l e in
          let dl = depth insL in
          let dr = depth r in
          let bal = dl - dr in
          if bal <> 2
            then Node ((max dr dl) + 1, insL, x, r)
          else if e < value l
            then balanceLL (Node (dl + 1, insL, x, r))
          else if e > value l
            then balanceLR (Node (dl + 1, insL, x, r))
          else t
        | Eq -> t
        | Gt ->
           let insR = insert r e in
           let dr = depth insR in
           let dl = depth l in
           let bal = dl - dr in
           if bal <> -2
             then Node ((max dr dl) + 1, l, x, insR)
           else if e > value r
             then balanceRR (Node (dr + 1, l, x, insR))
           else if e < value r
             then balanceRL (Node (dr + 1, l, x, insR))
           else t)
    | Leaf -> Node (1, Leaf, e, Leaf)

  let rec min tree = match tree with
    | Node (_, Leaf, x, _) -> x
    | Node (_, l, _, _) -> min l
    | Leaf -> failwith "Impossible"

  let left tree = match tree with
    | Node (_, l, _, _) -> l
    | Leaf -> failwith "Impossible"

  let rigth tree = match tree with
    | Node (_, _, _, r) -> r
    | Leaf -> failwith "Impossible"

end
