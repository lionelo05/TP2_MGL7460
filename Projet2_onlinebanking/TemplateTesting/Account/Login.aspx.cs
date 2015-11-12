using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using TemplateTesting.Models;

namespace TemplateTesting.Banque
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            //this.Master.FindControl("customer").Visible=false;
           // this.Master.FindControl("admin").Visible = false;
            
            ContactHyperLink.NavigateUrl = "Contact";
            OpenAuthLogin.ReturnUrl = Request.QueryString["ReturnUrl"];
            var returnUrl = HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
            if (!String.IsNullOrEmpty(returnUrl))
            {
                ContactHyperLink.NavigateUrl += "?ReturnUrl=" + returnUrl;
            }
        }

        protected void LogIn(object sender, EventArgs e)
        {
            if (IsValid)
            {
                // Valider le mot de passe de l'utilisateur
                var manager = new UserManager();
                try {
                ApplicationUser user = manager.Find(UserName.Text, Password.Text);

                //this.Master.FindControl("customer1").Visible=false;
                if (user != null)
                {
                   
                    IdentityHelper.SignIn(manager, user, RememberMe.Checked);
                    IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
                    //var nameuser = User.Identity.GetUserName;
                    
                }
                else
                {
                    //FailureText.Text = manager.GetType;
                    FailureText.Text = "Invalid username or password.";
                    ErrorMessage.Visible = true;
                }


                }
                catch (Exception excp) {
                    FailureText.Text = excp.Message;
                    ErrorMessage.Visible = true;
               }



            }
        }

        protected void RememberMe_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}