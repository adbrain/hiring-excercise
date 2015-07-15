# Time Series Analysis on Weather Data

To build this project and run unit tests:
``` mvn clean install ```

The specification called for 
- a NoSQL data store (I chose Cassandra)
- a parallel computation tool (I chose Spark)

### Setting up Cassandra

Before you can run the project to get descriptive statistics, we need to set up a Cassandra table for data storage. Assuming you have a Cassandra cluster running, create the schema using the contents of ``/schema/weather.cql``.

Then we want to copy over the weather data into our table. In my case, I pulled a year's worth of daily weather data from the National Climate Data Center and stored it in 2015.csv. Assuming your table is called raw_daily_weather under the test keyspace:

```
COPY test.raw_daily_weather (station_id, date, type, value, a, b, c, time) FROM '/data/2015.csv' with header=true and delimiter=',';
```

The table post-import should look something like:

```
cqlsh> select * from test.raw_daily_weather_small;
 station_id  | date     | type | a    | b    | c | time | value
-------------+----------+------+------+------+---+------+-------
 US1FLSL0019 | 20150101 | PRCP | null | null | N | null |   173
 USC00242347 | 20150101 | TMAX | null | null | H | 0800 |     0
 USC00242347 | 20150101 | TMIN | null | null | H | 0800 |  -133
```

### To Run
Now that the data store is set up, we can run the application. To run it locally (in this case, from the Spark installation directory):
```
./bin/spark-submit --class timeseries.WeatherTimeSeriesAnalysis --master local[1] weather-1.0-SNAPSHOT.jar 127.0.0.1 <cassandra-table> <cassandra-keyspace>
```

### Descriptive Statistics

The statistics gathered from the daily weather data include:
- minimum temperature average and maximum temperature average per location over all time
- min temp and max temp average per month for each location
- min temp and max temp average per year for each location

It is easy to extend the class structure to get slightly different statistics - only the "group by" column needs to be modified in ``WeatherDataLoader.aggregateValues``

### Unit test configuration
The configuration file for unit tests can be found at ``src/test/resources/spark.properties``. You may need to modify ``spark.driver.host`` to reflect your system's host address in order for the tests to run smoothly.


