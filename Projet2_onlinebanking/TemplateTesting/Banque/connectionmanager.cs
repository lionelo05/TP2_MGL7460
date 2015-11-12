using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;

namespace TemplateTesting.Banque
{

    public interface Iconnectionmanager 
    {
        //double credituser(string log);
    }




    public class connectionmanager:Iconnectionmanager
    {
       public const string DebitAmountExceedsBalanceMessage = "Debit amount exceeds balance";
        public const string DebitAmountLessThanZeroMessage = "Debit amount less than zero";

        //double debitamount = 0;
        //double creditamount = 0;
        double balance;
        double totalbal;

        string userlogin="";
        //string name = "";

        SqlDataAdapter da;

        DataSet ds = new DataSet();

        SqlCommand cmd = new SqlCommand();
       // ConfigurationManager cfm = new ConfigurationManager();
        public string connStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
       
        SqlConnection con;


        private Iconnectionmanager manager;

        // Constructor takes a stockfeed:
      
        public connectionmanager(Iconnectionmanager manage){
            manager=manage;
        }
        public connectionmanager(double total)
        {
            Totalbal = total;
        }
        public connectionmanager()
        {
           
        }
        public connectionmanager(String userlog, double balancecst)
        {
            userlogin = userlog;
            balance = balancecst;
        }

        public double Totalbal
        {
            get { return totalbal; }
            set { totalbal=0; }
        }
        public double Balance
        {
            get { return balance; }
            set { balance=0; }
        }
        public string Userlog
        {
            get {
                if (userlogin.Length < 6)
                {
                    throw new Exception ("La taille du user login est incorect");
                    //Console.WriteLine("erreur");
                }
                return userlogin; }
            set {
                userlogin = "molo";
            }
        }




        

        

    }
}