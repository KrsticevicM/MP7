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
    public class searchTest
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
        public static void performSearch(IWebDriver driver)
        {

            try
            {
                // Navigate to the login page
                driver.Navigate().GoToUrl("https://localhost:5173");
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

                // Find the username and password input fields and the login button
                IWebElement specie = driver.FindElement(By.Id("pet-species"));
                SelectElement selectSpecie = new SelectElement(specie);
                IWebElement nameInput = driver.FindElement(By.Id("pet-name"));
                IWebElement searchButton = driver.FindElement(By.Id("btn"));


               
                selectSpecie.SelectByText("Ptica");
                nameInput.SendKeys("Vincent");

                Thread.Sleep(5000);
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", searchButton);
                searchButton.Click();

                Thread.Sleep(5000); //wait so page loads correctly

                string fileName = @"C:\Users\Korisnik\Desktop\selenium\SeleniumWebDriver_MP7\SeleniumWebDriver_MP7\screenshots\search_successful.png";
                TakeScreenshot(driver, fileName);

            }
            catch (Exception ex)
            {
                string fileName = @"C:\Users\Korisnik\Desktop\selenium\SeleniumWebDriver_MP7\SeleniumWebDriver_MP7\screenshots\search_failed.png";
                TakeScreenshot(driver, fileName);
                Console.WriteLine(ex.Message);

            }


        }

        [Test]
        public void searcTests()
        {
            IWebDriver driver = new ChromeDriver();
            performSearch(driver);
            driver.Quit();
        }



    }




}

