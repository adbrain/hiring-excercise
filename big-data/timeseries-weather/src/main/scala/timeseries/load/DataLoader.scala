package timeseries.load

import org.apache.spark.sql.{DataFrame, SQLContext, Row}
import scala.reflect.ClassTag

/**
 * Handles the loading of a generic table from Cassandra
 * @param sqlContext SQL context used
 * @param tableName the table name in Cassandra
 * @param keyspaceName the keyspace in Cassandra
 */
abstract class DataLoader[T: ClassTag](sqlContext: SQLContext, tableName: String, keyspaceName: String) extends Serializable {

  val rawDataset: DataFrame = loadTable()

  /**
   * Loads the specified table from Cassandra
   * @return a DataFrame containing the rows
   */
  def loadTable(): DataFrame = {
    val cassandraOptions = Map("table" -> tableName, "keyspace" -> keyspaceName)
    sqlContext.read.format("org.apache.spark.sql.cassandra")
      .options(cassandraOptions).load
  }
}
