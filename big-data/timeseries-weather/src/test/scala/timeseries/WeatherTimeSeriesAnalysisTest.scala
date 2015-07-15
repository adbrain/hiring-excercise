package timeseries

import org.scalatest.{FlatSpec, BeforeAndAfter, Matchers}
import org.apache.spark.{SparkContext, SparkConf}
import org.apache.spark.sql.{DataFrame, Row, SQLContext}
import timeseries.load.WeatherDataLoader
import org.apache.spark.sql.types._
import org.apache.spark.sql.types.StructField
import com.typesafe.config.ConfigFactory

/**
 * Verify that the weather time series analysis functions perform as expected
 */
class WeatherTimeSeriesAnalysisTest extends FlatSpec with Matchers with BeforeAndAfter {

  private var sqlContext: SQLContext = _

  /**
   * Loads the spark.properties file to set up the spark and sql context
   */
  before {
    val props = ConfigFactory.load("spark.properties")
    val conf = new SparkConf().
      setMaster(props.getString("spark.master")).
      setAppName(props.getString("spark.app.name")).
      set("spark.driver.host", props.getString("spark.driver.host"))

    sqlContext = new SQLContext(new SparkContext(conf))
  }

  /**
   * Cleans up after the Spark SQL context
   */
  after {
    if (sqlContext.sparkContext != null) {
      sqlContext.sparkContext.stop()
    }

    System.clearProperty("spark.driver.port")
  }

  /**
   * Creates the schema for the raw weather data table in Cassandra
   * @return schema for raw weather data
   */
  private def schemaForWeatherDataTable(): StructType = {
    new StructType(
      Array(
        StructField("station_id", StringType, true),
        StructField("date", StringType, true),
        StructField("type", StringType, true),
        StructField("a", StringType, true),
        StructField("b", StringType, true),
        StructField("c", StringType, true),
        StructField("time", StringType, true),
        StructField("value", DecimalType(), true)))
  }

  /**
   * Creates a series of entries in the raw weather data, which should simulate real data:
   * - have some non-temperature element types like 'PRCP' (precipitation) and so on
   * - have 'TMAX' and 'TMIN'
   * - include multiple days, months and years in data
   * - include multiple locations (aka station ids)
   */
  private def createWeatherDataTable(): Seq[Row] = {
    Seq(
      Row("US1FLSL0019", "20150101", "PRCP", "", "", "N", "", new java.math.BigDecimal(173.0)),
      Row("US1FLSL0019", "20150101", "TMAX", "", "", "N", "", new java.math.BigDecimal(173.0)),
      Row("US1FLSL0019", "20150101", "TMIN", "", "", "N", "", new java.math.BigDecimal(173.0)),
      Row("US1FLSL0020", "20150101", "PRCP", "", "", "N", "", new java.math.BigDecimal(173.0)),
      Row("US1FLSL0019", "20150102", "TMAX", "", "", "N", "", new java.math.BigDecimal(173.0)),
      Row("US1FLSL0019", "20150102", "TMIN", "", "", "N", "", new java.math.BigDecimal(177.0)),
      Row("US1FLSL0019", "20150402", "TMIN", "", "", "N", "", new java.math.BigDecimal(17.0)),
      Row("US1FLSL0019", "20150402", "TMAX", "", "", "N", "", new java.math.BigDecimal(173.0)),
      Row("US1FLSL0020", "20150406", "TMAX", "", "", "N", "", new java.math.BigDecimal(-173.0)),
      Row("US1FLSL0019", "20150406", "TMIN", "", "", "N", "", new java.math.BigDecimal(173.0)),
      Row("US1FLSL0abcde", "20150102", "PRCP", "", "", "N", "", new java.math.BigDecimal(173.0))
    )
  }

  /**
   * Gets the cleaned up weather data given the raw weather data table
   * @return clean weather data
   */
  private def cleanWeatherData(): DataFrame = {
    val weatherDataTable = createWeatherDataTable()
    val rawData = sqlContext.createDataFrame(sqlContext.sparkContext.parallelize(weatherDataTable), schemaForWeatherDataTable())

    WeatherDataLoader.cleanWeatherData(rawData, sqlContext)
  }

  "Verify that cleanWeatherData" should "keep the columns and rows we care about" in {
    val cleanData = cleanWeatherData()
    cleanData.count() should equal(8)
  }

  "Check that aggregateValues" should "aggregate values for the right fields" in {
    val cleanData = cleanWeatherData()
    val aggResult = WeatherDataLoader.aggregateValues(cleanData, "elementType", "stationId", "date.month")
    aggResult.count() should equal(5)
  }
}
