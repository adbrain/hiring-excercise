package timeseries.load

import com.datastax.spark.connector._
import org.apache.spark.sql.{Row, DataFrame, SQLContext}
import com.github.nscala_time.time.Imports._
import org.apache.spark.rdd.RDD

/**
 * Represents a Date
 * @param year the date's year
 * @param month the date's month
 * @param day the date's day
 */
case class Date(val year: Int, val month: Int, val day: Int)

/**
 * Cleaned weather data containing information about the min/max temperature,
 * location and date
 * @param stationId the unique id associated with the station where the measurements were taken
 * @param date the date associated with the measurement
 * @param elementType the type of value being recorded (may be min temperature, max temperature etc)
 * @param value the value of the measurement (for temperature, it is in 10ths of degrees C)
 */
case class WeatherData(
    val stationId: String,
    val date: Date,
    val elementType: String,
    val value: BigDecimal) extends Serializable

/**
 * Loads the raw weather data from Cassandra and performs some necessary transformations
 * @param sqlContext SQL context used
 * @param tableName weather data table name in Cassandra
 * @param keyspaceName the keyspace the table is under
 */
class WeatherDataLoader(
    sqlContext: SQLContext,
    tableName: String,
    keyspaceName: String) extends DataLoader[WeatherData](sqlContext, tableName, keyspaceName) {

  val data = WeatherDataLoader.cleanWeatherData(rawDataset, sqlContext)

  /* Average temperature datasets after various aggregations */
  val avgTempsPerLocPerYear = WeatherDataLoader.aggregateValues(data, "elementType", "stationId", "date.year")
  val avgTempsPerLocPerMonth = WeatherDataLoader.aggregateValues(data, "elementType", "stationId", "date.month")
  val avgTempsPerLocPerDate = WeatherDataLoader.aggregateValues(data, "elementType", "stationId", "date")
  val avgTempsPerLoc = WeatherDataLoader.aggregateValues(data, "elementType", "stationId")
}

object WeatherDataLoader {

  /**
   * Deserializes a row in the raw weather data table into a WeatherData object
   * @param row a row in the raw weather data table
   * @return WeatherData object
   */
  def deserialize(row: Row): WeatherData = {
    val parsedDate = DateTimeFormat.forPattern("yyyyMMdd").parseDateTime(row.getAs[String]("date"))
    new WeatherData(
      row.getAs[String]("station_id"),
      Date(parsedDate.getYear, parsedDate.getMonthOfYear, parsedDate.getDayOfMonth),
      row.getAs[String]("type"),
      BigDecimal(row.getAs[java.math.BigDecimal]("value")))
  }

  /**
   * Cleans the raw weather by filtering the necessary columns
   */
  def cleanWeatherData(data: DataFrame, sqlContext: SQLContext): DataFrame = {
    val weatherRDD: RDD[WeatherData] = data.filter("type = 'TMAX' OR type = 'TMIN'")
      .select("station_id", "date", "type", "value")
      .map(row => deserialize(row))
    sqlContext.createDataFrame(weatherRDD)
  }

  /**
   * Returns the average of the 'value' field when various aggregations
   * are performed, as specified by the groupBy
   * @param groupBy specify how to group the dataset
   * @return average values for specific groupings
   */
  def aggregateValues(data: DataFrame, groupBy: String*): DataFrame = {
    data.groupBy(groupBy(0), groupBy.drop(1):_*).agg(Map(
      "value" -> "avg"
    ))
  }
}
