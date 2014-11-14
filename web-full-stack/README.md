# Task
Implement a solution to retrieve the top 100 items from the reddit API, save them to a database, then expose an API which returns the items filtered by domain and grouped by author.

# Specification
+ The server should expose an API endpoint:
  + Endpoint: GET /sports?domain='...'; Params: domain - Should equal to the domain property in the reddit response 
  + Example: /sports?domain='youtube.com'
  + Response: Returns an array which contains items grouped by author, must support JSON format.
  + Example Response
``` 
[
	{
		author: "Mike",
		items: [
			{
				id: "123fg",
				createdDate: "2014-09-14T22:44:51+00:00",
				title: "Huge hit in football game",
				permalink: "/r/sports/comments/2gl3ih/huge_hit_in_hs_football_game/" 
			}
			...
		]	
	}
...
]
```
+ When calling the API the data should be retrieved from reddit.Endpoint to use: http://www.reddit.com/r/sports.json?limit=100
+ The reddit API response should be saved to the database without restructuring the data. (renaming is allowed)
+ Before sending the response the data should be processed to return the posts grouped by the user

![Workflow](http://i.imgur.com/tWSyZKk.png?2 "Workflow")

# Context
Technology stack to use:
+ C#
+ ASP.NET WEB API 2.x
+ SQL Server
+ Any ORM technologies

# Goal
During implementation focus on delivering a high quality, maintainable, working solution with passing unit tests. No additional functionality is required. 