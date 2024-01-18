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
    public class ComponentTest
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

        [Test]
        public void RegistrationTests()
        {
            //testiramo email
            IWebDriver driver = new ChromeDriver();

            List<string> emails = new List<string>();

            // Add elements to the list
            emails.Add("valentino.boskovic@gmail.com"); //ispravno
            emails.Add("ivanhorvat.com"); //bez @
            emails.Add("@fer.com"); //bez usernamea samo domena
            emails.Add("horvatNina@.com"); //bez domene
            emails.Add("horvatNina@fer."); //bez ekstenzije

            foreach (string email in emails)
            {

                RegistrationTest.performRegistration(driver, email, "0992168933", "Valentino", "Boskovic");
                driver.Quit();
            }


            //testiramo phoneNum
            List<string> phoneNums = new List<string>();

            // Add elements to the list
            phoneNums.Add("0997421334"); //ispravno
            phoneNums.Add("099a78954$"); // unos slova i/ili znaka
            phoneNums.Add("099-742-1334"); //s crticama
            phoneNums.Add("099 742 1334"); //razmaci

            foreach (string phoneNum in phoneNums)
            {

                RegistrationTest.performRegistration(driver, "valentino.boskovic@gmail.com", phoneNum, "Valentino", "Boskovic");
                driver.Quit();
            }


            //testiramo firstName
            List<string> firstNames = new List<string>();

            // Add elements to the list

            firstNames.Add("Valentino"); //ispravno 
            firstNames.Add("Šime"); // unos slova è,æ,ž,š itd., takoðer ispravno
            firstNames.Add("Lana34"); //unos broja
            firstNames.Add("Lana€"); //unos simbola


            foreach (string firstName in firstNames)
            {

                RegistrationTest.performRegistration(driver, "valentino.boskovic@gmail.com", "0997421334", firstName, "Boskovic");
                driver.Quit();
            }


            //testiramo lastName
            List<string> lastNames = new List<string>();

            // Add elements to the list
            lastNames.Add("Boškoviæ"); //ispravno
            lastNames.Add("Horvat2"); // unos broja
            lastNames.Add("Horvat$$"); //unos znaka

            foreach (string lastName in lastNames)
            {

                RegistrationTest.performRegistration(driver, "valentino.boskovic@gmail.com", "0997421334", "Valentino", lastName);
                driver.Quit();
            }


            //testiramo ne unešenu sliku pri kreiranju oglasa

            Boolean unesiSliku = false;
            LoginTest.performLogin(driver, "danci", "kroksice");
            PostAdTest.AdTest(driver, unesiSliku, true);
            driver.Quit();


            //testiramo ne unešenu lokaciju pri kreiranju oglasa

            Boolean unesiLokaciju = false;
            LoginTest.performLogin(driver, "danci", "kroksice");
            PostAdTest.AdTest(driver, true, unesiLokaciju);
            driver.Quit();



        }



    }




}


