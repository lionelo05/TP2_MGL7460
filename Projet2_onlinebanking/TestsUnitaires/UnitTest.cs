using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TemplateTesting.Banque;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using StealFocus.MSTestExtensions;  //Nouvel extension de visual studio. contient la gestion des transactions sql, les exception, et les delais d'execution
using System.IO;  //pour specifier le chemin  a la base de donnees

namespace TestsUnitaire
{
    [TestClass]
    public class UnitTest: MSTestExtensionsException //a telecharger MSTestExtensions pour utiliser cette implementation
    {
        DataAccess da = null;

        [TestInitialize]
        public void TestSetup()
        {
            AppDomain.CurrentDomain.SetData("DataDirectory", Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data"));  //Dit au projet de test ou se trouve la base de donnees.
            da = new DataAccess();
        }
        [TestCleanup]
        public void TearDown()
        {
            da = null;
        }
  ///<summary>
 ///Test unitaire sur debiter compte
/// </summary>
        [TestMethod]
        [TestCategory("Database")]
        [TestTransaction]                //Vas effectuer une manipulation dans la base de donnees, et cette manipulation sera supprimer a la fin du tests. Il se note [RollBack] en Mbunit
        public void Debit_compte_test_echec_solde_insuffisant()
        {
            //arrange
            string client = "Lionelll1";
            double montant_debit = 300;
            connectionmanager testeur = new connectionmanager();

            //assert 1
            Assert.IsTrue(testeur.credituser(client).Equals(0), "Montant devrait etre = 0");  //verifi d'abord que la balance du user est =0, car il est ressemment creer

            //act
            testeur.insertdebittrans(client, montant_debit);

            //assert 2
            Assert.IsTrue(testeur.credituser(client).Equals(0), "Probleme sur debit echec (solde insuffisant) du montant");
        }

        [TestMethod]
        [TestCategory("Database")]
        [TestTransaction]                //Vas effectuer une manipulation dans la base de donnees, et cette manipulation sera supprimer a la fin du tests. Il se note [RollBack] en Mbunit
        public void Debit_compte_test_echec_montant_negatif()
        {
            //arrange
            string client = "Lionelll2";
            double montant_debit = -300;
            connectionmanager testeur = new connectionmanager();

            //assert 1
            Assert.IsTrue(testeur.credituser(client).Equals(0), "Montant devrait etre = 0");  //verifi d'abord que la balance du user est =0, car il est ressemment creer


            //act
            testeur.insertdebittrans(client, montant_debit);


            //assert 2
            Assert.IsTrue(testeur.credituser(client).Equals(0), "Probleme sur debit echec (montant negatif) du montant");
        }

        [TestMethod]
        [TestCategory("Database")]
        [TestTransaction]                //Vas effectuer une manipulation dans la base de donnees, et cette manipulation sera supprimer a la fin du tests. Il se note [RollBack] en Mbunit
        public void Debit_compte_test_echec_plafond_atteint()
        {
            //arrange
            string client = "Lionelll3";
            double montant_debit = 1001;
            connectionmanager testeur = new connectionmanager();

            //assert 1
            Assert.IsTrue(testeur.credituser(client).Equals(0), "Montant devrait etre = 0");  //verifi d'abord que la balance du user est =0, car il est ressemment creer


            //act
            testeur.insertdebittrans(client, montant_debit);


            //assert 2
            Assert.IsTrue(testeur.credituser(client).Equals(0), "Probleme sur debit echec (plafond) du montant");
        }

        [TestMethod]
        [TestCategory("Database")]
        [TestTransaction]                //Vas effectuer une manipulation dans la base de donnees, et cette manipulation sera supprimer a la fin du tests. Il se note [RollBack] en Mbunit
        public void Debit_compte_test_reussi()
        {
            //arrange
            string client = "Lionelll4";
            double montant_crediter = 300;
            double montant_debit = 200;
            connectionmanager testeur = new connectionmanager();

            //assert 1
            Assert.IsTrue(testeur.credituser(client).Equals(0), "Montant devrait etre = 0");  //verifi d'abord que la balance du user est =0, car il est ressemment creer


            //act
            testeur.insertcredittrans(client, montant_crediter);
            testeur.insertdebittrans(client, montant_debit);


            //assert 2
            Assert.IsTrue(testeur.credituser(client).Equals(100), "Probleme sur debit reussi du montant");
        }

  ///<summary>
 /// /// Test unitaire sur crediter compte
/// </summary>
        [TestMethod]
        [TestCategory("Database")]
        [TestTransaction]                //Vas effectuer une manipulation dans la base de donnees, et cette manipulation sera supprimer a la fin du tests. Il se note [RollBack] en Mbunit
        public void Crediter_compte_test_echec()
        {
            //arrange
            string client = "Lionellll5";
            double montant_crediter = -300;
            connectionmanager testeur = new connectionmanager();
          
            //assert 1
            Assert.IsTrue(testeur.credituser(client).Equals(0),"Montant devrait etre = 0");  //verifi d'abord que la balance du user est =0, car il est ressemment creer
    

            //act
            testeur.insertcredittrans(client, montant_crediter);
            

            //assert 2
            Assert.IsTrue(testeur.credituser(client).Equals(0),"Probleme sur l'incrementation du montant"); //retrait d'un montant negatif n'a pas marche, balance toujours a 0
        }

        [TestMethod]
        [TestCategory("Database")]
        [TestTransaction]
        public void Crediter_compte_test_reussi()
        {
            //arrange
            string client = "Lionellll6";
            double montant_crediter = 300;
            connectionmanager testeur = new connectionmanager();

            //assert 1
            Assert.IsTrue(testeur.credituser(client).Equals(0), "Montant devrait etre = 0");  //verifi d'abord que la balance du user est =0, car il est ressemment creer


            //act
            testeur.insertcredittrans(client, montant_crediter);


            //assert 2
            Assert.IsTrue(testeur.credituser(client).Equals(300), "Probleme sur l'incrementation du montant");
        }





    }
}
