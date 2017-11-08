using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Text;
using System.Windows.Forms;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;


namespace FacebookScraper
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnLog_Click(object sender, EventArgs e)
        {

            IWebDriver driver = new FirefoxDriver("C:\\Program Files");
            driver.Manage().Window.Position.X.Equals(0);
            driver.Manage().Window.Position.Y.Equals(0);
            driver.Url = "https://www.facebook.com";
            driver.FindElement(By.Id("email")).SendKeys(txtlogin.Text);
            driver.FindElement(By.Id("pass")).SendKeys(txtsenha.Text);
            driver.FindElement(By.Id("loginbutton")).Click();

        }
    }
}
