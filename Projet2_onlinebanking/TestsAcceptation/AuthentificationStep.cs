using System;
using System.Text.RegularExpressions;
using System.Threading;
using TechTalk.SpecFlow;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System.Text;
using System.Drawing;

namespace TestsAcceptation
{
    [Binding]
    public class AuthentificationStep
    {
        private IWebDriver driver;
        private StringBuilder verificationErrors;
        private string baseURL;
        private bool acceptNextAlert = true;

        [TestInitialize]
        public void SetupTest()
        {
            verificationErrors = new StringBuilder();
        }

        [TestCleanup]
        public void TeardownTest()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
            Assert.AreEqual("", verificationErrors.ToString());
        }



        [Given(@"je suis sur la page d’accueil du logiciel")]
        public void GivenJeSuisSurLaPageDAccueilDuLogiciel()
        {
            driver = new FirefoxDriver();
            driver.Manage().Window.Maximize();
            baseURL = "http://localhost:50208";
            driver.Navigate().GoToUrl(baseURL + "/");
        }

        [Given(@"je click sur se connecter")]
        public void GivenJeClickSurSeConnecter()
        {
            driver.FindElement(By.LinkText("Se connecter")).Click();
        }


        [When(@"je saisis le nom d’utilisateur suivant :  '(.*)'")]
        public void WhenJeSaisisLeNomDUtilisateurSuivant(string login)
        {
            driver.FindElement(By.Id("MainContent_UserName")).Clear();
            //admin
            if(login =="lionel"){
                driver.FindElement(By.Id("MainContent_UserName")).SendKeys("lionel");
            }
            if (login == "LIONEL")
            {
                driver.FindElement(By.Id("MainContent_UserName")).SendKeys("LIONEL");
            }
            //client
            if (login == "LeProf")
            {
                driver.FindElement(By.Id("MainContent_UserName")).SendKeys("LeProf");
            }
            if (login == "inconnu")
            {
                driver.FindElement(By.Id("MainContent_UserName")).SendKeys("inconnu");
            }
            
        }
        
        [When(@"je saisis le mot de passe suivant : '(.*)'")]
        public void WhenJeSaisisLeMotDePasseSuivant(string password)
        {
            driver.FindElement(By.Id("MainContent_Password")).Clear();
            //admin
            if (password == "lionelo05")
            {
                driver.FindElement(By.Id("MainContent_Password")).SendKeys("lionelo05");
            }
            if (password == "LIONELO05")
            {
                driver.FindElement(By.Id("MainContent_Password")).SendKeys("LIONELO05");
            }
            //client
            if (password == "inconnu")
            {
                driver.FindElement(By.Id("MainContent_Password")).SendKeys("inconnu");
            }
            
        }

        [When(@"je clique sur le bouton connexion")]
        public void WhenJeCliqueSurLeBoutonConnexion()
        {
            driver.FindElement(By.Name("ctl00$MainContent$ctl05")).Click();
        }

        [Then(@"Authentification '(.*)'")]
        public void ThenAuthentification(string message)
        {
            //admin
            if (message == "réussie et je suis dirigé vers mon espace d’administrateur")
            {
                Assert.IsTrue(IsElementPresent(By.Id("admin")));
            }
            if (message == "administrateur échouée et je reste sur la page de login")
            {
                Assert.IsTrue(driver.Url.Equals("http://localhost:50208/Account/Login"));
            }
            //client
            if (message == "réussie et je suis dirigé vers mon espace client")
            {
                Assert.IsTrue(IsElementPresent(By.Id("customer")));
            }
            if (message == "client échouée et je reste sur la page de login")
            {
                Assert.IsTrue(driver.Url.Equals("http://localhost:50208/Account/Login"));
            }
        }


        private bool IsElementPresent(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        private bool IsAlertPresent()
        {
            try
            {
                driver.SwitchTo().Alert();
                return true;
            }
            catch (NoAlertPresentException)
            {
                return false;
            }
        }

        private string CloseAlertAndGetItsText()
        {
            try
            {
                IAlert alert = driver.SwitchTo().Alert();
                string alertText = alert.Text;
                if (acceptNextAlert)
                {
                    alert.Accept();
                }
                else
                {
                    alert.Dismiss();
                }
                return alertText;
            }
            finally
            {
                acceptNextAlert = true;
            }
        }
    }
}
