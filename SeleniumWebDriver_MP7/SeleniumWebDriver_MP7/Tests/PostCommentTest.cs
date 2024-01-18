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
    public class PostCommentTest
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

        public static void CommentTest(IWebDriver driver)
        {
            try
            {
                driver.Navigate().GoToUrl("https://localhost:5173/21");

                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
               
                wait.Until(driver => driver.Url.Equals("https://localhost:5173/21"));

                Thread.Sleep(3000);

                IWebElement addComInput = driver.FindElement(By.Id("add-button"));
                addComInput.Click();

                IWebElement textareaInput = driver.FindElement(By.Id("floatingTextarea2"));
                IWebElement mapInput = driver.FindElement(By.ClassName("map-container"));
                IWebElement imgInput = driver.FindElement(By.ClassName("img-container"));
                IWebElement btnSubmit = driver.FindElement(By.TagName("submit"));

                textareaInput.SendKeys("This is my comment.");

                string imgPath = @"C:\Users\Korisnik\Desktop\selenium\SeleniumWebDriver_MP7\SeleniumWebDriver_MP7\Tests\cat.jpeg";
                imgInput.SendKeys(imgPath);

                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", mapInput);


                Thread.Sleep(5000);

                mapInput.Click();

                Thread.Sleep(5000);

                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", btnSubmit);
                btnSubmit.Click();

                Thread.Sleep(3000);

                wait.Until(driver => driver.Url.Equals("https://localhost:5173/21"));

                string fileName = @"C:\Users\Korisnik\Desktop\selenium\SeleniumWebDriver_MP7\SeleniumWebDriver_MP7\screenshots\comment_successful.png";
                TakeScreenshot(driver, fileName);

            }
            catch (Exception ex)
            {
                string fileName = @"C:\Users\Korisnik\Desktop\selenium\SeleniumWebDriver_MP7\SeleniumWebDriver_MP7\screenshots\comment_failed.png";
                TakeScreenshot(driver, fileName);
                Console.WriteLine(ex.Message);

            }


        }

        [Test]
        public void postCommentTests()
        {
            //primjer objavljivanja komentara bez Logina
            IWebDriver driver = new ChromeDriver();
            CommentTest(driver);
            driver.Quit();
        }


    }


}


