using System;
using System.IO;
using System.Collections;
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
        public SortedList _perfis;
        
        public Form1()
        {
            InitializeComponent();
            _perfis = new SortedList();
        }

        private void btnLog_Click(object sender, EventArgs e)
        {

            IWebDriver driver = new FirefoxDriver("C:\\Program Files");
            driver.Manage().Window.Position.X.Equals(0);
            driver.Manage().Window.Position.Y.Equals(0);
            driver.Manage().Window.Size.Equals(0);
            driver.Url = "https://www.facebook.com";
            driver.FindElement(By.Id("email")).SendKeys(txtlogin.Text);
            driver.FindElement(By.Id("pass")).SendKeys(txtsenha.Text);
            driver.FindElement(By.Id("loginbutton")).Click();

            learquivo();
            mostra_lista();

        }

        public void learquivo()
        {

            
            string line;
            StreamReader profiles = new StreamReader(@"profiles.txt");

            while (!profiles.EndOfStream)
            {
                line = profiles.ReadLine();
                var values = line.Split(';');
                Perfil _item = new Perfil();
                _item.Nome = values[0];
                _item.id = values[1];
                _item.Foto = values[2];
                _item.Universidade = values[3];
                _item.Local = values[4];
                _perfis.Add(_item.Nome, _item);
            }

            profiles.Close();
        }

        public void mostra_lista()
        {
            lB1.Items.Clear();

            foreach (DictionaryEntry item in _perfis)
                lB1.Items.Add(item.Value.ToString());
        }

        private void lB1_DoubleClick(object sender, EventArgs e)
        {
            string _item = lB1.SelectedItem.ToString();
            string nome = _item.Split('*')[0];
            string foto = _item.Split('*')[2];
            pictureBox1.Load(foto);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }
    }
}
