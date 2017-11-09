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
using OpenQA.Selenium.Chrome;
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
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("--disable-notifications", "--disable-infobars");
            IWebDriver driver = new ChromeDriver("C:\\Program Files", options);
            driver.Manage().Window.Position.X.Equals(0);
            driver.Manage().Window.Position.Y.Equals(0);
            driver.Manage().Window.Size.Equals(0);
            driver.Url = "https://www.facebook.com";
            driver.FindElement(By.Id("email")).SendKeys(txtlogin.Text);
            driver.FindElement(By.Id("pass")).SendKeys(txtsenha.Text);
            driver.FindElement(By.Id("loginbutton")).Click();

            int tamanho;
            tamanho = r.data.Count;

            //System.Threading.Thread.Sleep(5000);

            //for (int i = Convert.ToInt32(txtbloc.Text); i < tamanho; i++)
            for (int i = 15; i < tamanho; i++)
            {
                System.Threading.Thread.Sleep(2000);
                driver.Url = "https://www.facebook.com/" + r.data[i].id;
                driver.FindElement(By.XPath(".//*[@data-tab-key='about']")).Click();
                string currentURL = driver.Url;
                string eduURL = currentURL;
                string localURL = currentURL;

                string profilepic;

                try
                {
                    profilepic = driver.FindElement(By.CssSelector("[class='profilePic img']")).GetAttribute("src");
                }
                catch (Exception)
                {
                    profilepic = driver.FindElement(By.CssSelector("[class='profilePic silhouette img']")).GetAttribute("src");
                    continue;
                }


                localURL = localURL + "&section=living";
                //System.Threading.Thread.Sleep(2000);
                driver.Url = localURL;
                IList<IWebElement> all = driver.FindElements(By.TagName("a"));
                int pos = 0;
                for (int x = 0; x < all.Count; x++)
                {
                    if (all[x].Text == "Acontecimentos")
                        pos = x;
                }
                string city;
                if (all[pos + 1].Text == "Sobre")
                    city = "Brasil";
                else
                    city = Convert.ToString(all[pos + 1].Text);

                

                eduURL = eduURL + "&section=education";
                //System.Threading.Thread.Sleep(2000);
                driver.Url = eduURL;
                IWebElement bodyTag = driver.FindElement(By.TagName("body"));
                if (bodyTag.Text.Contains("Univale") || bodyTag.Text.Contains("UNIVALE") || bodyTag.Text.Contains("Universidade Vale do Rio Doce"))
                {
                    string path = @"profiles.txt";
                    string write_to_file = r.data[i].name + ';' + r.data[i].id + ';' + profilepic + ';' + "Univale" + ';' + city + ';' + Environment.NewLine;
                    if (!File.Exists(path))
                    { 
                        File.WriteAllText(path, write_to_file);
                    }
                    File.AppendAllText(path, write_to_file);
                }
                


            }
            driver.Quit();
            learquivo();
            mostra_lista();

        }

        public void learquivo()
        {

            
            string line;
            StreamReader profiles = new StreamReader(@"profiles_salvo.txt");

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
                try
                {
                    _perfis.Add(_item.Nome, _item);
                }
                catch (System.ArgumentException)
                {
                    continue;
                }
                
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
            gmap.SetPositionByKeywords(local);
            label3.Text = local;
            label4.Text = nome;
        }


    }
}
