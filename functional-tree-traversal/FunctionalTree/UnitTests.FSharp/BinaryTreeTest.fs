namespace Adbrain.FunctionalTree.UnitTests.FSharp

open FsUnit
open NUnit.Framework
open Adbrain.FunctionalTree.Engine

module BinaryTreeTest =

    type private Node =
        { Key : int
          Name : string
          Left : Node option
          Right : Node option }

    let private specs : BinaryTree.Specs<Node option> =
        { LeftPicker = fun x -> x.Value.Left
          RightPicker = fun x -> x.Value.Right
          IsEqual = fun x y -> x.Value.Key = y.Value.Key && x.Value.Name = y.Value.Name 
          IsLess = fun x y -> x.Value.Key <= y.Value.Key
          IsEmpty = fun x -> x.IsNone }

    [<TestFixture>]
    type ``BinaryTree: function find `` () =
        
        let x0 = Some({ Key = 0; Name = "Zero"; Left = None; Right = None })
        let x1 = Some({ Key = 1; Name = "One"; Left = None; Right = None })
        let x1' = Some({ Key = 1; Name = "One'"; Left = None; Right = None })
        let x2 = Some({ Key = 2; Name = "Two"; Left = None; Right = None })
        let x3 = Some({ Key = 3; Name = "Three"; Left = None; Right = None })
        let x4 = Some({ Key = 4; Name = "Four"; Left = None; Right = None })
        let x5 = Some({ Key = 5; Name = "Five"; Left = None; Right = None })

        let tree = 
            Some({ Key = 1
                   Name = "One"
                   Left = Some({ Key = 1
                                 Name = "One'"
                                 Left = Some({ Key = 0
                                               Name = "Zero"
                                               Left = None
                                               Right = None })
                                 Right = None })
                   Right = Some({ Key = 3
                                  Name = "Three"
                                  Left = x2
                                  Right = x4 }) })
                                               
        [<Test>] member test.
         ``on an empty tree returns None`` () =
            BinaryTree.find specs x1 None 
            |> should equal None
                   
        [<Test>] member test.
         ``on a tree with single node finds the node if it has same name`` () =
            BinaryTree.find specs x1 x1
            |> Option.get
            |> specs.IsEqual x1
            |> should be True

        [<Test>] member test.
         ``on a tree with single node returns None if the name is different`` () =
            BinaryTree.find specs x1' x1
            |> should equal None

        [<Test>] member test.
         ``on a tree with single node returns None if the key is different`` () =
            BinaryTree.find specs x1 x2
            |> should equal None

        [<Test>] member test.
         `` returns the correct node when there are duplicate keys`` () =
            BinaryTree.find specs x1' tree
            |> Option.get
            |> specs.IsEqual x1'
            |> should be True

        [<Test>] member test.
         `` returns the correct node when looking for node 0`` () =
            BinaryTree.find specs x0 tree
            |> Option.get
            |> specs.IsEqual x0
            |> should be True

        [<Test>] member test.
         `` returns the correct node when looking for node 1`` () =
            BinaryTree.find specs x1 tree
            |> Option.get
            |> specs.IsEqual x1
            |> should be True

        [<Test>] member test.
         `` returns the correct node when looking for node 2`` () =
            BinaryTree.find specs x2 tree
            |> Option.get
            |> specs.IsEqual x2
            |> should be True

        [<Test>] member test.
         `` returns the correct node when looking for node 3`` () =
            BinaryTree.find specs x3 tree
            |> Option.get
            |> specs.IsEqual x3
            |> should be True

        [<Test>] member test.
         `` returns the correct node when looking for node 4`` () =
            BinaryTree.find specs x4 tree
            |> Option.get
            |> specs.IsEqual x4
            |> should be True

        [<Test>] member test.
         `` returns None when looking for node 5`` () =
            BinaryTree.find specs x5 tree
            |> should equal None