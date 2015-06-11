# Tasks
* Implement a time series analysis on top of some historical weather data. 
* Implement a method in Scala 

Both tasks are required.

# Time series analysis
The purpose of this task is to model a data sink on a data storage of your choice to allow time series analysis and populate it with a substantial amount of daily weather data (Manageable on a single machine) from National Climate Data Center. Once the data is in place carry out an experimental time series analysis to get some descriptive statistics about a trend of climate change. 

##### Specification
+ A NoSQL table (Cassandra, HBase, DynamoDB) that captures location, timestamp, min temp and max temp.
+ Parallel computation tool (Java MapReduce, Pig, Cascading, Spark etc..) 

# Scala method
Implement the following method in Scala. The solution should be in a SBT project and some unit tests should be included.
``` 
/**
*
* @param num An integer and  is assumed to be at least 0.
* @return A list of integers with the first entry being num, and the subsequent ones
*         are gotten by omitting the left hand most digit one by one.
*         Eg  num=0 returns List(0).
*             num=1234 returns List(1234,234,34,4)
*/
def leftTruncate(num:Int):List[Int]
```