package test_pack_1;

import java.util.concurrent.TimeUnit;

import org.junit.Assert;
import org.openqa.selenium.*;
import org.openqa.selenium.firefox.FirefoxDriver;
import org.openqa.selenium.WebDriver;

public class bookAmazon {
	
    private static WebDriver driver = new FirefoxDriver();
    
    public void tcStart() {
    	
        driver.manage().timeouts().implicitlyWait(10, TimeUnit.SECONDS);
        
    // navigate to Amazon
        driver.get("http://wwww.amazon.com/");
        
    // Narrow search to Books
        driver.findElement(By.id("nav-link-shopall")).click();
        driver.findElement(By.xpath(
    		    "//*[@id='nav-flyout-shopAll']/div[2]/span[9]/span"
    		  )).click();
        driver.findElement(By.xpath(
    		    "//*[@id='nav-flyout-shopAll']/div[3]/div[9]/div[1]/a[1]/span"
    		  )).click();
                
    // Search for "hobbit"        
        driver.findElement(By.name("field-keywords")).sendKeys("hobbit");
        driver.findElement(By.className("nav-input")).click();

    // Find the book version of The Hobbit         
        driver.findElement(By.partialLinkText("The Hobbit")).click();
        
    // Add it to the cart        
        driver.findElement(By.id("add-to-cart-button")).click();
	
    // Verify the cart contains the book
        String bodyText = driver.findElement(By.id("huc-v2-order-row-confirm-text")).getText();
        Assert.assertTrue("Text not found!", bodyText.contains("Added to Cart") || bodyText.contains("added to Cart"));
        
    // close window
        driver.close();
        
    }
}
