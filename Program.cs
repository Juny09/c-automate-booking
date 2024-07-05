
using System;
using OpenQA.Selenium;
using SeleniumUndetectedChromeDriver;

class Program
{
    static void Main()
    {
        try
        {
            // Path to the ChromeDriver executable
            var driverExecutablePath = @"C:\Users\JUNYUAN\Desktop\chromedriver-win64\chromedriver-win64\chromedriver.exe";

            // Create an instance of the undetected Chrome driver
            using var driver = UndetectedChromeDriver.Create(driverExecutablePath: driverExecutablePath);

            // Navigate to the login page
            driver.Navigate().GoToUrl("");//enter url 

            // Wait for the page to load
            System.Threading.Thread.Sleep(2000);

            // Find the email/username field
            var emailField = driver.FindElement(By.Id("Username"));
            emailField.SendKeys("abcd@gmail.com"); // Replace with your actual username

            // Find the password field
            var passwordField = driver.FindElement(By.Id("Password"));
            passwordField.SendKeys("abcdabcd"); // Replace with your actual password

            // Find the login button
            var loginButton = driver.FindElement(By.XPath("//input[@type='submit']"));
            loginButton.Click();

            // Wait for the page to load after login
            System.Threading.Thread.Sleep(3000);

            // Navigate to the booking page
            var bookingLink = driver.FindElement(By.CssSelector("li.navBookings > a"));
            bookingLink.Click();

            // Wait for the booking page to load
            System.Threading.Thread.Sleep(3000);

            // Find and click on the Tennis Court link (assuming the structure is the same)
            var tennisCourtLink = driver.FindElement(By.CssSelector("a.facility-tile[href='booking/35']"));
            tennisCourtLink.Click();

            // Wait for the booking page to load
            System.Threading.Thread.Sleep(3000);

            //Book the specified time slots for the grass court on the specified date
            //BookTennisCourt(driver, "Tuesday");

            // Calculate next Tuesday's date
            DateTime nextTuesday = DateTime.Today.AddDays((DayOfWeek.Tuesday - DateTime.Today.DayOfWeek + 7) % 7);
            string nextTuesdayDate = nextTuesday.ToString("yyyy-MM-dd");


            // Book the specified time slots for the grass court on the next Tuesday
            BookTennisCourt(driver, nextTuesdayDate);

            // Wait for a while to observe the result
            System.Threading.Thread.Sleep(3000);

            // Close the browser
            //driver.Quit();

        }
        catch (Exception ex)
        {
            // Log the exception or handle it as necessary
            Console.WriteLine("An error occurred: " + ex.Message);
        }
    }

    static void BookTennisCourt(IWebDriver driver, string date)
    {
        try
        {
            // Select the date
            var dateElement = driver.FindElement(By.CssSelector($"td.fcDay.available[id='d{date}']"));
            dateElement.Click();

            // Wait for the page to update
            System.Threading.Thread.Sleep(2000);

            // Select the timeslot for Tennis 3 (Grass) court for the given time ranges
            var timeSlot1 = driver.FindElement(By.CssSelector($"td.fcTimeCell.available.f-71[id='t72_0900_1000_{date}']"));
            timeSlot1.Click();
            //var timeSlot2 = driver.FindElement(By.CssSelector($"td.fcTimeCell.available.f-71[id='t71_1000_1100_{date}']"));
            //timeSlot2.Click();

            // Confirm the booking if necessary (this step may vary based on the site's workflow)
            var bookButton = driver.FindElement(By.Id("cmd-bk-book"));
            bookButton.Click();

            // Wait for the booking to be processed
            System.Threading.Thread.Sleep(3000);

            // Confirm the booking if necessary (this step may vary based on the site's workflow)
            var bookFacButton = driver.FindElement(By.Id("dialog-book-cmd0"));
            bookFacButton.Click();

            // Wait for the booking to be processed
            System.Threading.Thread.Sleep(5000);
        }
        catch (Exception ex)
        {
            // Log the exception or handle it as necessary
            Console.WriteLine("An error occurred while booking: " + ex.Message);
        }
    }

}
