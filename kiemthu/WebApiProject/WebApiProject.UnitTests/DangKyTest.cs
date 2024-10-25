using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace WebApiProject.UnitTests
{
    [TestFixture]
    internal class DangKyTest 
    {
        private IWebDriver driver;    

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://localhost:44390/Register/Index"); 
        }

        [Test]
        public void Test_Register_Success()
        {
            
            driver.FindElement(By.Name("hoten")).SendKeys("Nguyen Van A");
            driver.FindElement(By.Name("email")).SendKeys("nguyenaavana@gmail.com");
            driver.FindElement(By.Name("soDienThoai")).SendKeys("0987654322");
            driver.FindElement(By.Name("diachi")).SendKeys("123 Đường ABC, TP.HCM");
            driver.FindElement(By.Name("password")).SendKeys("Khavo0603");
            driver.FindElement(By.Name("confirm_password")).SendKeys("Khavo0603");
           
            driver.FindElement(By.CssSelector("button[type='submit']")).Click();

            
            var successMessage = driver.FindElement(By.ClassName("alert-success")).Text;
            Assert.IsTrue(successMessage.Contains("Đăng ký thành công"));
        }

        [Test]
        public void Test_RegisterWithoutHoten()
        {

            driver.FindElement(By.Name("hoten")).SendKeys("");
            driver.FindElement(By.Name("email")).SendKeys("nguyenaavana@gmail.com");
            driver.FindElement(By.Name("soDienThoai")).SendKeys("0987654322");
            driver.FindElement(By.Name("diachi")).SendKeys("123 Đường ABC, TP.HCM");
            driver.FindElement(By.Name("password")).SendKeys("Khavo0603");
            driver.FindElement(By.Name("confirm_password")).SendKeys("Khavo0603");


            driver.FindElement(By.CssSelector("button[type='submit']")).Click();
            Thread.Sleep(2000);

            var err = driver.FindElements(By.XPath("//span[contains(@class, 'text-danger') and text()='Bạn chưa nhập họ tên!']"));


            string mes = err.Count > 0 ? err[0].Text : string.Empty;


            Assert.IsTrue(mes.Contains("Bạn chưa nhập họ tên!"));
        }


        [Test]
        public void Test_RegisterWithoutEmail()
        {
            
            driver.FindElement(By.Name("hoten")).SendKeys("Nguyen Van B");
            driver.FindElement(By.Name("email")).SendKeys(""); 
            driver.FindElement(By.Name("soDienThoai")).SendKeys("0987654322");
            driver.FindElement(By.Name("diachi")).SendKeys("123 Đường ABC, TP.HCM");
            driver.FindElement(By.Name("password")).SendKeys("Khavo0603");
            driver.FindElement(By.Name("confirm_password")).SendKeys("Khavo0603");

           
            driver.FindElement(By.CssSelector("button[type='submit']")).Click();
            Thread.Sleep(2000); 

            var err = driver.FindElements(By.XPath("//span[contains(@class, 'text-danger') and text()='Bạn chưa nhập Email!']"));

            
            string mes= err.Count > 0 ? err[0].Text : string.Empty;

            
            Assert.IsTrue(mes.Contains("Bạn chưa nhập Email!"));
        }

        [Test]
        public void Test_RegisterWithErrEmail()
        {

            driver.FindElement(By.Name("hoten")).SendKeys("Nguyen Van B");
            driver.FindElement(By.Name("email")).SendKeys("nguyenvanb123");
            driver.FindElement(By.Name("soDienThoai")).SendKeys("0987654322");
            driver.FindElement(By.Name("diachi")).SendKeys("123 Đường ABC, TP.HCM");
            driver.FindElement(By.Name("password")).SendKeys("Khavo0603");
            driver.FindElement(By.Name("confirm_password")).SendKeys("Khavo0603");


            driver.FindElement(By.CssSelector("button[type='submit']")).Click();
            Thread.Sleep(2000);

            var err = driver.FindElements(By.XPath("//span[contains(@class, 'text-danger') and text()='Email không hợp lệ.']"));


            string mes = err.Count > 0 ? err[0].Text : string.Empty;


            Assert.IsTrue(mes.Contains("Email không hợp lệ."));
        }

        [Test]
        public void Test_RegisterWithoutsoDienThoai()
        {

            driver.FindElement(By.Name("hoten")).SendKeys("Nguyễn Văn D");
            driver.FindElement(By.Name("email")).SendKeys("nguyenvand@gmail.com");
            driver.FindElement(By.Name("soDienThoai")).SendKeys("");
            driver.FindElement(By.Name("diachi")).SendKeys("123 Đường ABC, TP.HCM");
            driver.FindElement(By.Name("password")).SendKeys("Khavo0603");
            driver.FindElement(By.Name("confirm_password")).SendKeys("Khavo0603");


            driver.FindElement(By.CssSelector("button[type='submit']")).Click();
            Thread.Sleep(2000);

            var err = driver.FindElements(By.XPath("//span[contains(@class, 'text-danger') and text()='Số điện thoại là trường bắt buộc.']"));


            string mes = err.Count > 0 ? err[0].Text : string.Empty;


            Assert.IsTrue(mes.Contains("Số điện thoại là trường bắt buộc."));
        }

        [Test]
        public void Test_RegisterErrsoDienThoai()
        {

            driver.FindElement(By.Name("hoten")).SendKeys("Nguyễn Văn D");
            driver.FindElement(By.Name("email")).SendKeys("nguyenvand@gmail.com");
            driver.FindElement(By.Name("soDienThoai")).SendKeys("123456789");
            driver.FindElement(By.Name("diachi")).SendKeys("123 Đường ABC, TP.HCM");
            driver.FindElement(By.Name("password")).SendKeys("Khavo0603");
            driver.FindElement(By.Name("confirm_password")).SendKeys("Khavo0603");


            driver.FindElement(By.CssSelector("button[type='submit']")).Click();
            Thread.Sleep(2000);

            var err = driver.FindElements(By.XPath("//span[contains(@class, 'text-danger') and text()='Số điện thoại không hợp lệ.']"));


            string mes = err.Count > 0 ? err[0].Text : string.Empty;


            Assert.IsTrue(mes.Contains("Số điện thoại không hợp lệ."));
        }
        [Test]
        public void Test_RegisterWithoutAddress()
        {

            driver.FindElement(By.Name("hoten")).SendKeys("Nguyễn Văn D");
            driver.FindElement(By.Name("email")).SendKeys("nguyenvand@gmail.com");
            driver.FindElement(By.Name("soDienThoai")).SendKeys("0903654735");
            driver.FindElement(By.Name("diachi")).SendKeys("");
            driver.FindElement(By.Name("password")).SendKeys("Khavo0603");
            driver.FindElement(By.Name("confirm_password")).SendKeys("Khavo0603");

             
            driver.FindElement(By.CssSelector("button[type='submit']")).Click();
            Thread.Sleep(2000);

            var err = driver.FindElements(By.XPath("//span[contains(@class, 'text-danger') and text()='Bạn chưa nhập địa chỉ!']"));


            string mes = err.Count > 0 ? err[0].Text : string.Empty;


            Assert.IsTrue(mes.Contains("Bạn chưa nhập địa chỉ!"));
        }

        [Test]
        public void Test_RegisterWithoutPassword()
        {

            driver.FindElement(By.Name("hoten")).SendKeys("Nguyễn Văn D");
            driver.FindElement(By.Name("email")).SendKeys("nguyenvand@gmail.com");
            driver.FindElement(By.Name("soDienThoai")).SendKeys("123456789");
            driver.FindElement(By.Name("diachi")).SendKeys("123 Đường ABC, TP.HCM");
            driver.FindElement(By.Name("password")).SendKeys("");
            driver.FindElement(By.Name("confirm_password")).SendKeys("");


            driver.FindElement(By.CssSelector("button[type='submit']")).Click();
            Thread.Sleep(2000);

            var err = driver.FindElements(By.XPath("//span[contains(@class, 'text-danger') and text()='Bạn chưa nhập mật khẩu']"));


            string mes = err.Count > 0 ? err[0].Text : string.Empty;


            Assert.IsTrue(mes.Contains("Bạn chưa nhập mật khẩu"));
        }

        [Test]
        public void Test_RegisterErrtPassword()
        {

            driver.FindElement(By.Name("hoten")).SendKeys("Nguyễn Văn D");
            driver.FindElement(By.Name("email")).SendKeys("nguyenvand@gmail.com");
            driver.FindElement(By.Name("soDienThoai")).SendKeys("123456789");
            driver.FindElement(By.Name("diachi")).SendKeys("123 Đường ABC, TP.HCM");
            driver.FindElement(By.Name("password")).SendKeys("123abc");
            driver.FindElement(By.Name("confirm_password")).SendKeys("123abc");


            driver.FindElement(By.CssSelector("button[type='submit']")).Click();
            Thread.Sleep(2000);

            var err = driver.FindElements(By.XPath("//span[contains(@class, 'text-danger') and text()='Mật khẩu tối thiểu 8 ký tự, có chữ hoa, chữ thường và số']"));


            string mes = err.Count > 0 ? err[0].Text : string.Empty;


            Assert.IsTrue(mes.Contains("Mật khẩu tối thiểu 8 ký tự, có chữ hoa, chữ thường và số"));
        }

        [Test]
        public void Test_RegisterErrtConfirm_password()
        {

            driver.FindElement(By.Name("hoten")).SendKeys("Nguyễn Văn D");
            driver.FindElement(By.Name("email")).SendKeys("nguyenvand@gmail.com");
            driver.FindElement(By.Name("soDienThoai")).SendKeys("123456789");
            driver.FindElement(By.Name("diachi")).SendKeys("123 Đường ABC, TP.HCM");
            driver.FindElement(By.Name("password")).SendKeys("Khavo0603");
            driver.FindElement(By.Name("confirm_password")).SendKeys("Khavo");


            driver.FindElement(By.CssSelector("button[type='submit']")).Click();
            Thread.Sleep(2000);

            var err = driver.FindElements(By.XPath("//span[contains(@class, 'text-danger') and text()='Mật khẩu không khớp']"));


            string mes = err.Count > 0 ? err[0].Text : string.Empty;


            Assert.IsTrue(mes.Contains("Mật khẩu không khớp"));
        }

        [TearDown]
        public void Teardown()
        {
            
            if (driver != null)
            {
                //Thread.Sleep(6000);
                driver.Quit();
                driver.Dispose();
            }
        }

     
      
    }
}
