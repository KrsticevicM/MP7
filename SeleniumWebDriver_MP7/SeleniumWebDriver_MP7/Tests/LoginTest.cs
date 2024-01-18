using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace SeleniumWebDriver_MP7.Tests
{
    public class LoginTest
    {
        private static void TakeScreenshot(IWebDriver driver, string fileName)
        {
            Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();
            ImageFormat format = ImageFormat.Jpeg;
            using (MemoryStream imageStream = new MemoryStream(screenshot.AsByteArray))
            {
                using (FileStream fileStream = new FileStream(fileName, FileMode.Create))
                {
                    using (Image screenshotImage = Image.FromStream(imageStream))
                    {
                        screenshotImage.Save(fileStream, format);
                    }
                }
            }
        }
        public static void performLogin(IWebDriver driver, string username, string password)
        { 

            try
            {
                // Navigate to the login page
                driver.Navigate().GoToUrl("https://localhost:5173/login");

                // Find the username and password input fields and the login button
                IWebElement usernameInput = driver.FindElement(By.Id("floatingInput"));
                IWebElement passwordInput = driver.FindElement(By.Id("floatingPassword"));
                IWebElement loginButton = driver.FindElement(By.Id("btn"));


                usernameInput.SendKeys(username);
                passwordInput.SendKeys(password);

                // Click the login button
                loginButton.Click();

                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));


                wait.Until(driver => driver.Url.Equals("https://localhost:5173/"));

                Thread.Sleep(3000); //wait so page loads correctly

                string fileName = @"C:\Users\Korisnik\Desktop\selenium\SeleniumWebDriver_MP7\SeleniumWebDriver_MP7\screenshots\login_successful.png";
                TakeScreenshot(driver, fileName);

            }
            catch (Exception ex)
            {
                string fileName = @"C:\Users\Korisnik\Desktop\selenium\SeleniumWebDriver_MP7\SeleniumWebDriver_MP7\screenshots\login_failed.png";
                TakeScreenshot(driver, fileName);
                Console.WriteLine(ex.Message);

            }


        }

        [Test]
        public void LoginTests()
        {
            IWebDriver driver = new ChromeDriver();
            performLogin(driver, "dusko", "miamia");
            driver.Quit();
        }



    }




}

