package exercise

/**
 * Handles the left-truncate method
 */
object LeftTruncate {

  /**
   * @param num An integer and is assumed to be at least 0.
   * @return A list of integers with the first entry being num, and the subsequent ones
   *         are gotten by omitting the left hand most digit one by one.
   *         Eg  num=0 returns List(0).
   *             num=1234 returns List(1234,234,34,4)
   */
  def leftTruncate(num: Int): List[Int] = {
    val numStr = num.toString
    (0 until numStr.length).toList.map (x => numStr.substring(x).toInt)
  }
}
