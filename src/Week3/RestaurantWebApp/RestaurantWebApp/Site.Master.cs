using SimpleWebApp.Models;
using SimpleWebApp.Services;
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SimpleWebApp
{
    public partial class SiteMaster : MasterPage
    {
        private readonly IAuthService _auth = new AuthService();

        protected void Page_Load(object sender, EventArgs e)
        {
            // Clear placeholder to avoid duplicates on postback
            phUser.Controls.Clear();

            var token = Request.Cookies["auth_token"]?.Value;
            var principal = _auth.ValidateJwt(token);

            if (principal != null)
            {
                var username = principal.Identity?.Name ?? "User";

                // Wrapper
                var container = new Panel { CssClass = "user-section" };

                var lblWelcome = new Label
                {
                    Text = $"Welcome, {username}",
                    CssClass = "welcome-label"
                };

                var btnLogout = new Button
                {
                    ID = "btnLogout",
                    Text = "Logout",
                    CssClass = "btn logout-btn"
                };
                btnLogout.Click += BtnLogout_Click;

                container.Controls.Add(lblWelcome);
                container.Controls.Add(btnLogout);

                phUser.Controls.Add(container);
            }

            else
            {
                var lnkLogin = new HyperLink
                {
                    NavigateUrl = "~/Views/login.aspx",
                    Text = "Login",
                    CssClass = "login-link"
                };

                phUser.Controls.Add(lnkLogin);
            }
        }

        private void BtnLogout_Click(object sender, EventArgs e)
        {
            if (Request.Cookies["auth_token"] != null)
            {
                var expired = new HttpCookie("auth_token", "")
                {
                    Expires = DateTime.UtcNow.AddDays(-1),
                    Path = "/" 
                };
                Response.Cookies.Add(expired);
            }

            Response.Redirect("~/Views/index.aspx");
        }

    }
}
