# Task
Implement a solution to receive a POST request with the following properties:
+ Age (int)
+ Name (string)

Save these in a database as a binary tree, keyed by age, and expose an HTTP endpoint to query the tree by a persons name and age. The query should run a tree traversal algorithm to find the correct node in the tree. The code to query the tree should be written in a functional manner.

# Specification
+ Expose a POST endpoint /people to insert data
+ Expose a GET endpoint to query for people /people?name=Bob&age=29
+ Use a functional approach for the tree search algorithm.

# Tech stack
+ C# and/or F#
+ Any database of your choice
+ ASP.NET WEB API 2.x

# Goal
Focus on structuring the tree properly in a database of your choosing. You will need to support insert and find methods for the tree. Also focus on separating  the data access logic from the tree traversal logic and include some tests were necessary.

+ Receives JSON {Name: “Bob”, Age: 29 }
+ Returns { Name: “Bob”, Age: 29 }