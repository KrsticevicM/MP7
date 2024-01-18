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
    public class PostAdTest
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
       
        public static void AdTest(IWebDriver driver)
        {
            try
            {
                IWebElement myAdsButton = driver.FindElement(By.Id("btn-moji-oglasi"));

                myAdsButton.Click();

                Thread.Sleep(3000);

                IWebElement newAddButton = driver.FindElement(By.Id("add-button"));

                newAddButton.Click();


                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));


                wait.Until(driver => driver.Url.Equals("https://localhost:5173/newAd"));

                IWebElement fileInput = driver.FindElement(By.Id("img"));
                IWebElement imeInput = driver.FindElement(By.Id("ime"));
                IWebElement specie = driver.FindElement(By.Id("vrsta"));
                SelectElement selectSpecie = new SelectElement(specie);
                IWebElement checkbox = driver.FindElement(By.Name("crna-boja"));
                IWebElement dropdownAge = driver.FindElement(By.Id("age"));
                SelectElement selectAge = new SelectElement(dropdownAge);
                IWebElement dateInput = driver.FindElement(By.Id("datum-nestanka"));
                IWebElement timeInput = driver.FindElement(By.Id("vrijeme-nestanka"));
                IWebElement gradInput = driver.FindElement(By.Id("grad-nestanka"));
                IWebElement element = driver.FindElement(By.Id("map-container"));

                IWebElement descInput = driver.FindElement(By.Id("opis"));
                IWebElement btnSubmit = driver.FindElement(By.Id("btn-createAd"));

                string imgPath = @"C:\Users\Korisnik\Desktop\selenium\SeleniumWebDriver_MP7\SeleniumWebDriver_MP7\Tests\cat.jpeg";
                fileInput.SendKeys(imgPath);

                imeInput.SendKeys("Jura");
                selectSpecie.SelectByText("Mačka");

                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                js.ExecuteScript("arguments[0].click();", checkbox);
                selectAge.SelectByText("6-10 god.");
                dateInput.SendKeys("12-11-2023");
                timeInput.SendKeys("14:30");
                gradInput.SendKeys("Makarska");

                descInput.SendKeys("jura juri");

                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", element);
                

                Thread.Sleep(5000);

                element.Click();

                Thread.Sleep(5000);

                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", btnSubmit);
                btnSubmit.Click();

                Thread.Sleep(5000);

                wait.Until(driver => driver.Url.Equals("https://localhost:5173/moji-oglasi"));

                string fileName = @"C:\Users\Korisnik\Desktop\selenium\SeleniumWebDriver_MP7\SeleniumWebDriver_MP7\screenshots\createAd_successful.png";
                TakeScreenshot(driver, fileName);

            }
            catch (Exception ex)
            {
                string fileName = @"C:\Users\Korisnik\Desktop\selenium\SeleniumWebDriver_MP7\SeleniumWebDriver_MP7\screenshots\createAd_failed.png";
                TakeScreenshot(driver, fileName);
                Console.WriteLine(ex.Message);

            }


        }

        [Test]
        public void postAdTests()
        {
            IWebDriver driver = new ChromeDriver();
            LoginTest.performLogin(driver, "dusko", "miamia");
            AdTest(driver);
            driver.Quit();
        }


    }


}

