using System;
using System.Text.RegularExpressions;
using System.Threading;
using TechTalk.SpecFlow;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System.Text;

namespace TestsAcceptation
{
    [Binding]
    public class Ajout_ClientSteps
    {
        private IWebDriver driver;
        private StringBuilder verificationErrors;
        private string baseURL;
        private bool acceptNextAlert = true;
        private int nb_clt_initial = 3;

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

        
        [Given(@"je suis sur la page d’accueil du logiciel1")]
        public void GivenJeSuisSurLaPageDAccueilDuLogiciel()
        {
            driver = new FirefoxDriver();
            driver.Manage().Window.Maximize();
            baseURL = "http://localhost:50208";
            driver.Navigate().GoToUrl(baseURL + "/");
        }

        [Given(@"je click sur se connecter1")]
        public void GivenJeClickSurSeConnecter()
        {
            driver.FindElement(By.LinkText("Se connecter")).Click();
        }

        [Given(@"je saisis le nom d’utilisateur suivant1 :  '(.*)'")]
        public void GivenJeSaisisLeNomDUtilisateurSuivant(string login)
        {
            if (login == "lionel")
            {
                driver.FindElement(By.Id("MainContent_UserName")).SendKeys("lionel");
            }
        }
        
        [Given(@"je saisis le mot de passe suivant1 : '(.*)'")]
        public void GivenJeSaisisLeMotDePasseSuivant(string password)
        {
            if (password == "lionelo05")
            {
                driver.FindElement(By.Id("MainContent_Password")).SendKeys("lionelo05");
            }
        }
        
        [Given(@"je clique sur le bouton connexion1")]
        public void GivenJeCliqueSurLeBoutonConnexion()
        {
            driver.FindElement(By.Name("ctl00$MainContent$ctl05")).Click();
        }
        
        [Given(@"je suis dans l'espace administrateur1")]
        public void GivenJeSuisDansLEspaceAdministrateur()
        {
            Assert.IsTrue(IsElementPresent(By.Id("admin")));
        }
        
        [Given(@"je click sur Gerer comptes clients")]
        public void GivenJeClickSurGererComptesClients()
        {
            driver.FindElement(By.LinkText("Gerer comptes clients")).Click();
        }
        
        
        [Given(@"la banque a un total de (.*) client")]
        public void GivenLaBanqueAUnTotalDeClient(int nbclt)
        {
            if (nbclt == nb_clt_initial)
            {
                try
                {
                    Assert.AreEqual(Convert.ToString(nbclt), driver.FindElement(By.Id("MainContent_totalclt")).Text);
                }
                catch (UnitTestAssertException e)
                {
                    verificationErrors.Append(e.Message);
                }
            }
        }
        
        [When(@"je clique sur ajouter un nouveau client\.")]
        public void WhenJeCliqueSurAjouterUnNouveauClient_()
        {
            driver.FindElement(By.LinkText("Ajouter client")).Click();
        }

        [When(@"je saisis le login '(.*)', le mot de passe, le nom, le prénom, '(.*)' et d’autres informations du client X\.")]
        public void WhenJeSaisisLeLeMotDePasseLeNomLePrenomEtDAutresInformationsDuClientX_(string login, string courriel)
        {
            if (login == "Ricardo")
            {
                driver.FindElement(By.Id("MainContent_UserName")).Clear();
                driver.FindElement(By.Id("MainContent_UserName")).SendKeys("Ricardo");
                driver.FindElement(By.Id("MainContent_Password")).Clear();
                driver.FindElement(By.Id("MainContent_Password")).SendKeys("lionelo05");
                driver.FindElement(By.Id("MainContent_ConfirmPassword")).Clear();
                driver.FindElement(By.Id("MainContent_ConfirmPassword")).SendKeys("lionelo05");
                driver.FindElement(By.Name("ctl00$MainContent$ctl08")).Click();
                driver.FindElement(By.Id("MainContent_addname")).Clear();
                driver.FindElement(By.Id("MainContent_addname")).SendKeys("Ricardo");
                driver.FindElement(By.Id("MainContent_addlname")).Clear();
                driver.FindElement(By.Id("MainContent_addlname")).SendKeys("Don");
                driver.FindElement(By.Id("MainContent_addemail")).Clear();
                driver.FindElement(By.Id("MainContent_addemail")).SendKeys("yazid@gmail.com");
                driver.FindElement(By.Id("MainContent_addphone")).Clear();
                driver.FindElement(By.Id("MainContent_addphone")).SendKeys("4249383");
                driver.FindElement(By.Id("MainContent_addadresse")).Clear();
                driver.FindElement(By.Id("MainContent_addadresse")).SendKeys("rue mtl");
                driver.FindElement(By.Id("MainContent_addbirthdate")).Clear();
                driver.FindElement(By.Id("MainContent_addbirthdate")).SendKeys("1991-06-05");
            }
        }
        
        [When(@"je clique sur submit\.")]
        public void WhenJeCliqueSurSubmit_()
        {
            driver.FindElement(By.Id("MainContent_btnsubmit")).Click();
        }
        
        [Then(@"la banque a  maintenant un total de (.*) client")]
        public void ThenLaBanqueAMaintenantUnTotalDeClient(int nbclt)
        {
            if (nbclt == nb_clt_initial+1)
            {
                try
                {
                    Assert.AreEqual(Convert.ToString(nbclt), driver.FindElement(By.Id("MainContent_totalclt")).Text);
                }
                catch (UnitTestAssertException e)
                {
                    verificationErrors.Append(e.Message);
                }
            }
        }
        
        [Then(@"le client '(.*)' apparait dans la liste des clients")]
        public void ThenLeClientApparaitDansLaListeDesClients(string client)
        {
            if (client == "Alexandro")
            {
                try
                {
                    Assert.AreEqual(client, driver.FindElement(By.XPath("//table[@id='MainContent_Grid']/tbody/tr[4]/td[2]")).Text);
                }
                catch (UnitTestAssertException e)
                {
                    verificationErrors.Append(e.Message);
                }
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
