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
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using System.Net;
using System.Collections.Generic;
using OpenQA.Selenium.Support.UI;

namespace FacebookScraper
{
    public partial class Form1 : Form
    {
        public SortedList _perfis;
        public RootObject r;

        public Form1()
        {
            InitializeComponent();
            _perfis = new SortedList();

            //WebRequest request = WebRequest.Create("https://graph.facebook.com/v2.10/261065417267565/members?access_token=EAACEdEose0cBACKQNyS0GtnCqoMdJI4k3ZBVnSOe9FZC06oxfFBoKBMEJVPCubDOP1b9CsNyynFv2DODQbUdlIZBLlWIfq9HSpRnZChXHvvrDDrlBtFRzShSIUDy0KlqLipu9BIXKZCstbZBN7Hebkb35a4hmhg8KkBGTdVD1fxSZAJXzei1VluaTo7VAVeywlaIMJzzvFfCt7oirH9eZAAe");
            //WebResponse response = request.GetResponse();
            //Stream data = response.GetResponseStream();
            string html = String.Empty;
            using (StreamReader sr = new StreamReader(@"json20.txt"))
            {
                html = sr.ReadToEnd();
            }
            r = JsonConvert.DeserializeObject<RootObject>(html);
        }


        private void btnLog_Click(object sender, EventArgs e)
        {

            //MessageBox.Show(Convert.ToString(r.data.Count));


            IWebDriver driver = new FirefoxDriver("C:\\Program Files");
            driver.Manage().Window.Position.X.Equals(0);
            driver.Manage().Window.Position.Y.Equals(0);
            driver.Manage().Window.Size.Equals(0);
            driver.Url = "https://www.facebook.com";
            driver.FindElement(By.Id("email")).SendKeys(txtlogin.Text);
            driver.FindElement(By.Id("pass")).SendKeys(txtsenha.Text);
            driver.FindElement(By.Id("loginbutton")).Click();


            //driver.Manage().Timeouts().SetPageLoadTimeout(5);
            int tamanho;
            tamanho = r.data.Count;
            for (int i = 0; i < tamanho; i++)
            {
                driver.Url = "https://www.facebook.com/" + r.data[i].id;
                driver.FindElement(By.XPath(".//*[@data-tab-key='about']")).Click();
                //driver.FindElement(By.XPath(".//*[@data-hovercard-prefer-more-content-show='1']")); 
                //driver.FindElement(By.XPath(".//*[@data-testid='nav_edu_work']")).Click(); 
                //while (true)
                //{
                //    var allTextBoxes = driver.FindElement(By.ClassName("profileLink"));
                //    foreach (var cidade in allTextBoxes)
                //    {
                //        cidade.DoSomething();
                //    }
                //    IWebElement body = driver.FindElement(By.TagName("body"));
                //    if (body.Text.Contains("Mora em " + cidade))
                //    {
                //        MessageBox.Show("Univale" + cidade);
                //        break;
                //    }
                //}

                IList<IWebElement> all = driver.FindElements(By.TagName("a"));
                foreach (var item in all)
                {
                    MessageBox.Show(item.Text);
                }
                
   
                //String[] allText = new String[all.Count];
                //int iterator = 0;
                //IWebElement bodysearch = driver.FindElement(By.TagName("body"));
                //foreach (IWebElement element in all)
                //{
                //    MessageBox.Show(element.Text);
                //    allText[iterator++] = element.Text;
                //}
                //foreach (String item in allText)
                //{
                //    MessageBox.Show(item);
                //    //if (bodysearch.Text.Contains("Mora em " + item))
                //    //{
                //    //    
                //    //    break;
                //    //}
                //}


                string currentURL = driver.Url;
                currentURL = currentURL + "&section=education";
                driver.Url = currentURL;
                IWebElement bodyTag = driver.FindElement(By.TagName("body"));
                if (bodyTag.Text.Contains("Univale"))
                {
                    MessageBox.Show("Univale");
                    break;
                }
                    
            }

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
            string local = _item.Split('*')[4];
            pictureBox1.Load(foto);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            gmap.Visible = true;
            gmap.MapProvider = GMap.NET.MapProviders.BingMapProvider.Instance;
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly;
            if (local == "")
                local = "Brasil";
            else
                local = local + ", Brasil";
            gmap.SetPositionByKeywords(local);
            label3.Text = local;
            label4.Text = nome;
        }


    }
}
