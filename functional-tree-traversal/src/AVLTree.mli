
(* AVL tree: self balancing binary tree *)
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

type order = Lt | Eq | Gt
module type Ord =
sig
  type t
  val compare : t -> t -> order
end

module Make (Elem : Ord) : AVLTree with type elem = Elem.t
