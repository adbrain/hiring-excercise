namespace Adbrain.FunctionalTree.Engine

open Adbrain.DataAccess.Entities

module PersonTree =
    
    let private specs : BinaryTree.Specs<PersonNode> =  
        { LeftPicker = fun p -> p.LeftChild
          RightPicker = fun p -> p.RightChild 
          IsEqual = fun p1 p2 -> p1.Age = p2.Age && p1.Name.ToLower() = p2.Name.ToLower()
          IsLess = fun p1 p2 -> p1.Age <= p2.Age
          IsEmpty = fun p -> obj.ReferenceEquals(p, null) }

    let Find(name : string, age : int, tree : PersonNode) =
        let nodeToSearch = PersonNode(Name = name, Age = age)
        match BinaryTree.find specs nodeToSearch tree with
        | Some(node) -> node
        | None -> null