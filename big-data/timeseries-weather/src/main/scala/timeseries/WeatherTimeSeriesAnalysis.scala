package timeseries

import org.apache.spark.{SparkContext, SparkConf}
import org.apache.spark.sql.SQLContext
import timeseries.load.WeatherDataLoader
import timeseries.storage.StatsStorer

/**
 * Performs time series analysis on weather data
 */
object WeatherTimeSeriesAnalysis {

  /**
   * Does time series analysis on weather data stored in Cassandra
   * @param args as follows:
   * [0] - Cassandra connection host (ex: "127.0.0.1")
   * [1] - Cassandra table name that holds raw weather data
   * [2] - Cassandra keyspace
   */
  def main(args: Array[String]) {
    val conf = createSparkConf(args)
    val sqlContext = new SQLContext(new SparkContext(conf))
    val weatherDataLoader = new WeatherDataLoader(sqlContext, args(1), args(2))

    saveStats(weatherDataLoader)
    sqlContext.sparkContext.stop()
  }

  private def createSparkConf(args: Array[String]): SparkConf = {
    new SparkConf()
      .setAppName("Weather Data Load")
      .set("spark.cassandra.connection.host", args(0))
  }

  def saveStats(weatherDataLoader: WeatherDataLoader): Unit = {
    val prefix = "dailyWeather"
    new StatsStorer(weatherDataLoader.avgTempsPerLocPerMonth, prefix + "AvgTempsPerLocPerMonth").store()
    new StatsStorer(weatherDataLoader.avgTempsPerLocPerYear, prefix + "AvgTempsPerLocPerYear").store()
    new StatsStorer(weatherDataLoader.avgTempsPerLoc, prefix + "AvgTempsPerLoc").store()
  }
}
