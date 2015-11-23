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
using System.Collections.Specialized;


namespace TemplateTesting.Banque
{
    public partial class Listbalances : System.Web.UI.Page
    {
       
        connectionmanager gestionaire = new connectionmanager();
        string nom;

   
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.QueryString.HasKeys())
                {
                   // addclient = Server.UrlDecode(Request.QueryString["client"].Trim());
                    ajout.Visible = false;
                    infoajout.Visible = true;
                    enabletexbox();
                   // verifieajout = true;
                    gestionaire.BindData(Grid); 
                }
                else {
                   //verifieajout = false;
                   gestionaire.BindData(Grid);
                   
                }
                total.Text = Convert.ToString(gestionaire.Totalbalance()) + " $";
                totalclt.Text= Convert.ToString(gestionaire.Totalclient());
           } 

        }
        

        protected void Grid_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {

            Grid.CurrentPageIndex = e.NewPageIndex;

            gestionaire.BindData(Grid);

        }

        protected void Grid_EditCommand(object source, DataGridCommandEventArgs e)
        {

            Grid.EditItemIndex = e.Item.ItemIndex;

            gestionaire.BindData(Grid);

        }

        protected void Grid_CancelCommand(object source, DataGridCommandEventArgs e)
        {

            Grid.EditItemIndex = -1;

            gestionaire.BindData(Grid);

        }

        protected void Grid_DeleteCommand(object source, DataGridCommandEventArgs e)
        {

            gestionaire.deleteaccount(Grid, e);

        }

        protected void Grid_UpdateCommand(object source, DataGridCommandEventArgs e)
        {

            //erreur.Text=  e.Item.Cells[4].Controls[0]).Text);

            gestionaire.updateaccount(Grid, e);

        }

        protected void btnsubmit_Click(object sender, EventArgs e)
        {

            

            if (Request.QueryString.HasKeys())
            {
                float balance = 0;
                float debit = 0;
                float credit = 0;
                DateTime naiss = Convert.ToDateTime(addbirthdate.Text);
                string userlogin = Server.UrlDecode(Request.QueryString["client"].Trim());

                gestionaire.insertaccount(Grid, userlogin, addname.Text, addlname.Text, addemail.Text, addphone.Text, addadresse.Text, naiss, DateTime.Now, balance, debit, credit);
                gestionaire.createaccount(userlogin);
               // gestionaire.BindData(Grid);
                //emptytexbox();
                //ajout.Visible = true;
                //infoajout.Visible = false;
               // desabletexbox();
                //verifieajout = false;
                Response.Redirect("~/Banque/Listbalances");
            }
            else{
                erreur.Text = "Echec insertion";
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {

            emptytexbox();

        }

        public void emptytexbox()
        {
            addname.Text = "";
            addlname.Text = "";
            addemail.Text = "";
            addphone.Text = "";
            addadresse.Text = "";
            addbirthdate.Text = "";
        }
        protected void enabletexbox()
        {
            addname.Enabled=true;
            addlname.Enabled = true;
            addemail.Enabled = true;
            addphone.Enabled = true;
            addadresse.Enabled = true;
            addbirthdate.Enabled = true;
        }
        protected void desabletexbox()
        {
            addname.Enabled = false;
            addlname.Enabled = false;
            addemail.Enabled = false;
            addphone.Enabled = false;
            addadresse.Enabled = false;
            addbirthdate.Enabled = false;
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {

           // BindData1();
            //string userlogin = Server.UrlDecode(Request.QueryString["client"].Trim());
            //gestionaire.createaccount("test");
            

        }
 

    }
}