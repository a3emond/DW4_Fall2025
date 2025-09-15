using System;
using SimpleWebApp.Services;

namespace SimpleWebApp.Views
{
    public partial class Register : System.Web.UI.Page
    {
        private readonly IUserService _users = new UserService();

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text.Trim();
            string confirm = txtConfirm.Text.Trim();

            if (password != confirm)
            {
                lblError.Text = "Passwords do not match.";
                return;
            }

            var result = _users.Register(username, email, password);
            if (result.Success)
            {
                Response.Redirect("/login");
            }
            else
            {
                lblError.Text = result.Message ?? "Registration failed.";
            }
        }
    }
}
