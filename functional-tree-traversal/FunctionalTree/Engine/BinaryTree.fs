namespace Adbrain.FunctionalTree.Engine

module BinaryTree =

    type Specs<'a> = 
        { LeftPicker : 'a -> 'a
          RightPicker : 'a -> 'a
          IsEqual : 'a -> 'a -> bool
          IsLess : 'a -> 'a -> bool
          IsEmpty : 'a -> bool }
          
    let rec find specs x head =
        match head with
        | node when specs.IsEmpty node -> None
        | node when specs.IsEqual x node -> Some(x)
        | node -> 
            let nextNode = 
                if specs.IsLess x node 
                then specs.LeftPicker node
                else specs.RightPicker node
            find specs x nextNode
