package test_pack_1;


public class testcase_executor {

	public static void main(String[] args) {

		bookAmazon bookOrder = new bookAmazon();
		
		// Amazon TC 1
		bookOrder.tcStart();
		
		// Amazon TC 2
		// ...
		
		System.out.println("Session passed!");
		
	}

}
