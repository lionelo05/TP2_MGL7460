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
                
            }

        }


        
    }
}