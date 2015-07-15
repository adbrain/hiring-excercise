package exercise

import org.scalatest._

class LeftTruncateTest extends FlatSpec {

  "The leftTruncate method" should "create a list of integers where each truncates the left-most digit of the previous" in {
    assert(LeftTruncate.leftTruncate(1234).canEqual(List(1234, 234, 34, 4)))
    assert(LeftTruncate.leftTruncate(11112222).canEqual(List(11112222, 1112222, 112222, 12222, 2222, 222, 22, 2)))
  }

  "For an integer with one digit, leftTruncate" should "create a list with that int" in {
    (0 to 9).foreach {
      value => assert(LeftTruncate.leftTruncate(value).canEqual(List(value)))
    }
  }

  "For integers with digits that are 0, leftTruncate" should "contain values in its result list that are the same" in {
    assert(LeftTruncate.leftTruncate(1000).canEqual(List(1000, 0, 0, 0)))
    assert(LeftTruncate.leftTruncate(909090).canEqual(List(909090, 9090, 9090, 90, 90, 0)))
  }

  "For an int's max value, leftTruncate" should "have one integer per digit in the resulting list" in {
    val result: List[Int] = LeftTruncate.leftTruncate(Int.MaxValue)
    assert(result.length == 10)
    assert(result.canEqual(List(2147483647, 147483647, 47483647, 7483647, 483647, 83647, 3647, 647, 47, 7)))
  }
}