# Task
Implement a time series analysis on top of some historical weather data. 

# Goal
The purpose of this task is to model a data sink on a data storage of your choice to allow time series analysis and populate it with a substantial amount of daily weather data (Manageable on a single machine) from National Climate Data Center. Once the data is in place carry out an experimental time series analysis to get some descriptive statistics about a trend of climate change. 

# Specification
+ A NoSQL table (Cassandra, HBase, DynamoDB) that captures location, timestamp, min temp and max temp.
+ Parallel computation tool (Java MapReduce, Pig, Cascading, Spark etc..)
