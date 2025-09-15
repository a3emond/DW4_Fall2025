using System;
using SimpleWebApp.Services;

namespace SimpleWebApp.Views
{
    public partial class Login : System.Web.UI.Page
    {
        private readonly IUserService _users = new UserService();

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text.Trim();

            var result = _users.Login(email, password);

            if (result.Success)
            {
                // Save JWT in cookie
                Response.Cookies["auth_token"].Value = result.Token;
                Response.Cookies["auth_token"].HttpOnly = true;
                Response.Cookies["auth_token"].Expires = DateTime.UtcNow.AddHours(1);

                Response.Redirect("~/Views/index.aspx");
            }
            else
            {
                lblError.Text = result.Error ?? result.Message ?? "Login failed.";
            }
        }
    }
}
