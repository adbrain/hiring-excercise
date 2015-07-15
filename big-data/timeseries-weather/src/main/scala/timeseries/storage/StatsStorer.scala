package timeseries.storage

import org.apache.spark.sql.DataFrame

/**
 * Stores stats in CSV format
 */
class StatsStorer(data: DataFrame, location: String) {

  def store(): Unit = {
    data.write.format("com.databricks.spark.csv").save(location)
  }

}
