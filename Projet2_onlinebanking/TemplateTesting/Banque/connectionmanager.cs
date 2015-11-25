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
        double credituser(string log);
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




        public void BindData(DataGrid Grid)
        {
            con = new SqlConnection(connStr);
            con.Open();
            cmd.CommandText = "Select * From userAccount";

            cmd.Connection = con;

            da = new SqlDataAdapter(cmd);

            da.Fill(ds);

            cmd.ExecuteNonQuery();

            Grid.DataSource = ds;
            //Grid.Columns.

            Grid.DataBind();

            con.Close();
        }

        public void BindDatauser(DataGrid Grid, string nom)
        {
            userlogin = nom;
            con = new SqlConnection(connStr);
            con.Open();

            cmd.CommandText = "Select id,datetrans,credit,debit,balance FROM UserTransaction where userid='" + userlogin + "'";

            cmd.Connection = con;

            da = new SqlDataAdapter(cmd);

            da.Fill(ds);

            cmd.ExecuteNonQuery();

            Grid.DataSource = ds;

            Grid.DataBind();

            con.Close();
        }

        public double Totalbalance()
        {
            con = new SqlConnection(connStr);
            con.Open();

            cmd.CommandText = "SELECT SUM(balance) FROM UserAccount ";

            cmd.Connection = con;


            totalbal = Convert.ToDouble(cmd.ExecuteScalar());
            if (totalbal <0)                                    //tres important sur expliquation des messages d'erreur
            {
                throw new ArgumentOutOfRangeException("Probleme ici! le solde de la banque est en dessous de 0 !!!");
            }else{
            con.Close();
            return totalbal;
            }
        }

        public double Totalclient()
        {
            con = new SqlConnection(connStr);
            con.Open();

            cmd.CommandText = "SELECT COUNT(id) FROM UserAccount ";

            cmd.Connection = con;


            double totalclt = Convert.ToDouble(cmd.ExecuteScalar());

            con.Close();
            return totalclt;
        }

        public DateTime Dateouverturecompte(string log)
        {
            userlogin = log;
            con = new SqlConnection(connStr);
            con.Open();

            cmd.CommandText = "SELECT dateopen FROM UserAccount where userid='" + userlogin + "'";

            cmd.Connection = con;


            DateTime dat = Convert.ToDateTime(cmd.ExecuteScalar());

            con.Close();
            return dat;
        }

        public double credituser(string log)
        {
            userlogin = log;
            con = new SqlConnection(connStr);
            con.Open();

            //total += mt;
            //aggregate_function
            cmd.CommandText = "SELECT TOP 1 balance FROM UserTransaction where userid='" + userlogin + "' ORDER BY id DESC";

            cmd.Connection = con;


            double balanceactuel = Convert.ToDouble(cmd.ExecuteScalar());
            balance = balanceactuel;
           if(balance==200){                                           //juste pour voir si je peu entrer ici. 
               throw new ArgumentOutOfRangeException("mock marche");
             }
            con.Close();
            return balance;
        }

        public void createaccount(string userid)
        {
            userlogin = userid;

            con = new SqlConnection(connStr);

            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "Insert into UserTransaction (userid,credit,debit,balance,datetrans,dateopen) values('" + userlogin + "', 0, 0, 0, '" + DateTime.Now + "', '" + DateTime.Now + "')";
            cmd.Connection = con;

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

        }
        public void insertcredittrans(string loginuser, double credit)
        {
            userlogin = loginuser;
            double balance = credituser(loginuser);

            

            if (credit < 0)
            {
                //throw new ArgumentOutOfRangeException("amount invalid");
            }

            else{

            balance += credit;

            
                  con = new SqlConnection(connStr);
                  con.Open();

                  cmd.CommandType = System.Data.CommandType.Text;
                  cmd.CommandText = "Insert into UserTransaction (userid,credit,debit,balance,datetrans) values('" + userlogin + "','" + credit + "', '0', '" + balance + "','" + DateTime.Now + "')";
                  cmd.Connection = con;
                  cmd.ExecuteNonQuery();


                  cmd.CommandText = "Update UserAccount set debit=(SELECT SUM(debit) FROM UserTransaction WHERE userid='" + userlogin + "'), credit=(SELECT SUM(credit) FROM UserTransaction WHERE userid='" + loginuser + "'), balance='" + balance + "' where userid='" + loginuser + "' ";                                         
                  cmd.Connection = con;
                  cmd.ExecuteNonQuery();

                  con.Close();
             
            }

        }
        

        public void insertdebittrans(string loginuser, double debit)
        {
            userlogin = loginuser;
            double balance = credituser(loginuser);

            if (debit > balance)
            {
                //throw new ArgumentOutOfRangeException("amount", debit, DebitAmountExceedsBalanceMessage);
            }

            else if (debit < 0)
            {
               // throw new ArgumentOutOfRangeException("amount", debit, DebitAmountLessThanZeroMessage);
            }
            else if (debit > 1000)
            {
                //throw new ArgumentOutOfRangeException("amount", debit, DebitAmountLessThanZeroMessage);
            }

            else{

            balance -= debit;

            con = new SqlConnection(connStr);
            con.Open();

            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "Insert into UserTransaction (userid,credit,debit,balance,datetrans) values('" + userlogin + "', '0','" + debit + "', '" + balance + "','" + DateTime.Now + "')";
            cmd.Connection = con;
            cmd.ExecuteNonQuery();

            cmd.CommandText = "Update UserAccount set debit=(SELECT SUM(debit) FROM UserTransaction WHERE userid='" + userlogin + "'), credit=(SELECT SUM(credit) FROM UserTransaction WHERE userid='" + loginuser + "'), balance='" + balance + "' where userid='" + loginuser + "' ";
            cmd.Connection = con;
            cmd.ExecuteNonQuery();
            con.Close();
            }

        }

        public void deleteaccount(DataGrid Grid, DataGridCommandEventArgs e)
        {

            con = new SqlConnection(connStr);

            cmd.Connection = con;

            //int userId = (int)Grid.DataKeys[(int)e.Item.ItemIndex];

            cmd.CommandText = "Delete from UserAccount where Id=" + Grid.DataKeys[e.Item.ItemIndex];

            cmd.Connection.Open();

            cmd.ExecuteNonQuery();

            cmd.Connection.Close();

            Grid.EditItemIndex = -1;

            BindData(Grid);

        }

        public void updateaccount(DataGrid Grid, DataGridCommandEventArgs e)
        {

            con = new SqlConnection(connStr);

            //cmd.Parameters.Add("@id", SqlDbType.Int).Value = Convert.ToInt16(((TextBox)e.Item.Cells[0].Controls[0]).Text);
            //int iduser = Grid.DataKeys[e.Item.ItemIndex];
           

            cmd.Parameters.Add("@F_name", SqlDbType.VarChar).Value = ((TextBox)e.Item.Cells[2].Controls[0]).Text;

            cmd.Parameters.Add("@L_name", SqlDbType.VarChar).Value = ((TextBox)e.Item.Cells[3].Controls[0]).Text;

            cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = ((TextBox)e.Item.Cells[4].Controls[0]).Text;

            cmd.Parameters.Add("@phone", SqlDbType.VarChar).Value = ((TextBox)e.Item.Cells[5].Controls[0]).Text;

            cmd.Parameters.Add("@adress", SqlDbType.VarChar).Value = ((TextBox)e.Item.Cells[6].Controls[0]).Text;

            cmd.Parameters.Add("@birthdate", SqlDbType.DateTime).Value = ((TextBox)e.Item.Cells[7].Controls[0]).Text;

            cmd.Parameters.Add("@debit", SqlDbType.Int).Value = Convert.ToInt64 (((TextBox)e.Item.Cells[10].Controls[0]).Text);

            cmd.Parameters.Add("@credit", SqlDbType.Int).Value = Convert.ToInt64 (((TextBox)e.Item.Cells[11].Controls[0]).Text);

            cmd.CommandText = "Update UserAccount set F_Name=@F_name, L_name=@L_Name, email=@email, phone=@phone, adress=@adress, birthdate=@birthdate, debit=@debit, credit=@credit where id="+Grid.DataKeys[e.Item.ItemIndex];

            cmd.Connection = con;

            cmd.Connection.Open();

            cmd.ExecuteNonQuery();

            cmd.Connection.Close();

            Grid.EditItemIndex = -1;

            BindData(Grid);

        }

        public void insertaccount(DataGrid Grid, string userid, string addname, string addlname, string addemail, string addphone, string addadresse, DateTime addbirthdate, DateTime opendate, float balance, float debit, float credit)
        {

            userlogin = userid;
            con = new SqlConnection(connStr);

            //con.Open();
            //formu.FindControl()
            //cmd;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "Insert into UserAccount (userid,F_name,L_name,email,phone,adress,birthdate,dateopen,balance,debit,credit) values('" + userlogin + "', '" + addname + "', '" + addlname + "', '" + addemail + "', '" + addphone + "', '" + addadresse + "', '" + addbirthdate + "', '" + opendate + "','" + balance + "', '" + debit + "', '" + credit + "')";
            cmd.Connection=con;

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

        }

    }
}