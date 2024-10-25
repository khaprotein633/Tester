using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;


namespace WebApiProject.UnitTests
{
     [TestFixture]
    public class DangNhapTest
    {

        private IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://localhost:44390/Login/Index");
        }

        [Test]
        public void Test_Login_succees() {
            driver.FindElement(By.Name("email")).SendKeys("khavo0603@gmail.com");
            driver.FindElement(By.Name("pass")).SendKeys("123lol456");
            Thread.Sleep(2000);
            driver.FindElement(By.CssSelector("button[type='submit']")).Click();

            string expectedUrl = "https://localhost:44390/Home/Index";
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            Assert.That(expectedUrl, Is.EqualTo(driver.Url));
        }

        [Test]
        public void Test_Login_With_ErrEmail()
        {
            driver.FindElement(By.Name("email")).SendKeys("khavo@gmail.com");
            driver.FindElement(By.Name("pass")).SendKeys("123lol456");
            Thread.Sleep(2000);
            driver.FindElement(By.CssSelector("button[type='submit']")).Click();

            var successMessage = driver.FindElement(By.ClassName("alert-success")).Text;
            Assert.IsTrue(successMessage.Contains("Email hoặc mật khẩu không đúng!"));
        }
    

        [Test]
        public void Test_Login_WithoutEmail_Password()
        {
            driver.FindElement(By.Name("email")).SendKeys("");
            driver.FindElement(By.Name("pass")).SendKeys("");
            Thread.Sleep(2000);
            driver.FindElement(By.CssSelector("button[type='submit']")).Click();

            var successMessage = driver.FindElement(By.ClassName("alert-success")).Text;
            Assert.IsTrue(successMessage.Contains("Email hoặc mật khẩu không đúng!"));
        }

        [Test]
        public void Test_Login_With_ErrPassword()
        {
            driver.FindElement(By.Name("email")).SendKeys("khavo0603@gmail.com");
            driver.FindElement(By.Name("pass")).SendKeys("123456");
            Thread.Sleep(2000);
            driver.FindElement(By.CssSelector("button[type='submit']")).Click();

            var successMessage = driver.FindElement(By.ClassName("alert-success")).Text;
            Assert.IsTrue(successMessage.Contains("Email hoặc mật khẩu không đúng!"));
        }

        [TearDown]
        public void Teardown()
        {

            if (driver != null)
            {
                Thread.Sleep(6000);
                driver.Quit();
                driver.Dispose();
            }
        }
    }
}
