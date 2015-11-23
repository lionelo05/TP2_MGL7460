using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;

namespace TemplateTesting.Banque
{
    public partial class customerspace : System.Web.UI.Page
    {
        connectionmanager gestionaire = new connectionmanager();
        
  
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string login1 = Context.User.Identity.GetUserName();
                gestionaire.BindDatauser(Grid, login1);
                dat.Text= Convert.ToString(gestionaire.Dateouverturecompte(login1));
            }

        }


        protected void Grid_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            string login1 = Context.User.Identity.GetUserName();
            Grid.CurrentPageIndex = e.NewPageIndex;

            gestionaire.BindDatauser(Grid, login1);

        }


        protected void btncredit_Click(object sender, EventArgs e)
        {
            
            string login1 = Context.User.Identity.GetUserName();
            try
            {

            gestionaire.insertcredittrans(login1, Convert.ToDouble(montant.Text));
            gestionaire.BindDatauser(Grid, login1);
             }
           catch (Exception excp){
               erreur.Text = excp.Message;
           }

            montant.Text = "";
        }

        protected void btndebit_Click(object sender, EventArgs e)
        {
            string login1 = Context.User.Identity.GetUserName();
            gestionaire.insertdebittrans(login1, Convert.ToDouble(montant.Text));
            gestionaire.BindDatauser(Grid, login1);
            montant.Text = "";
        }
        
    }
}