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
    public class RegistrationTest
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
        public static void performRegistration(IWebDriver driver, string email, string phoneNum, string firstName, string lastName)
        {

            try
            {

                driver.Navigate().GoToUrl("https://localhost:5173/registration");

                IWebElement usernameInput = driver.FindElement(By.Id("floatingInput"));
                IWebElement passwordInput = driver.FindElement(By.Id("floatingPassword"));
                IWebElement emailInput = driver.FindElement(By.Id("floatingEmail"));
                IWebElement phoneNumInput = driver.FindElement(By.Id("floatingPhoneNumber"));
                IWebElement typeOfUser = driver.FindElement(By.Id("typeOfUser"));
                SelectElement select = new SelectElement(typeOfUser);

                //ako registriramo sklonište samo By.Id("floatingShelterName") i zakomentiramo LastNameInput
                IWebElement firstNameInput = driver.FindElement(By.Id("floatingFirstName"));
                IWebElement LastNameInput = driver.FindElement(By.Id("floatingLastName"));

                IWebElement registrationButton = driver.FindElement(By.Id("btn"));

                usernameInput.SendKeys("valentino_b");
                passwordInput.SendKeys("blojskaRapsodija123");
                emailInput.SendKeys(email);
                phoneNumInput.SendKeys(phoneNum);

                //ako zelimo sklonište - "Sklonište"
                select.SelectByText("Korisnik");

                firstNameInput.SendKeys(firstName);
                LastNameInput.SendKeys(lastName);

                Thread.Sleep(5000);

                registrationButton.Click();

                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                wait.Until(driver => driver.Url.Equals("https://localhost:5173/login"));

                Thread.Sleep(3000); //wait so page loads correctly

                string fileName = @"C:\Users\Korisnik\Desktop\selenium\SeleniumWebDriver_MP7\SeleniumWebDriver_MP7\screenshots\registration_successful.png";
                TakeScreenshot(driver, fileName);

            }
            catch (Exception ex)
            {
                string fileName = @"C:\Users\Korisnik\Desktop\selenium\SeleniumWebDriver_MP7\SeleniumWebDriver_MP7\screenshots\registration_failed.png";
                TakeScreenshot(driver, fileName);
                Console.WriteLine(ex.Message);

            }


        }

        [Test]
        public void RegistrationTests()
        {
            IWebDriver driver = new ChromeDriver();
            string email = "valentino.boskovic@gmail.com";
            string phoneNum = "0992168933";
            string firstName = "Valentino";
            string lastName = "Boskovic";
            performRegistration(driver, email, phoneNum, firstName, lastName);
            driver.Quit();
        }



    }




}


