using System;
using System.Data.SqlClient;
using System.Web;

namespace SimpleWebApp
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            try
            {
                string connStr = System.Configuration.ConfigurationManager
                    .ConnectionStrings["main_db"].ConnectionString;

                using (var conn = new SqlConnection(connStr)) //using statement -> connection will be closed and disposed automatically
                {
                    conn.Open();
                    Console.WriteLine("Database connection successful!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Database connection failed: " + ex.Message);
            }
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            string path = Request.Path.ToLower();

            if (path == "/" || path == "/index")
            {
                Response.ContentType = "text/html";
                Response.WriteFile("~/Views/index.html");
                Response.End();
            }
            else if (path == "/login")
            {
                Response.ContentType = "text/html";
                Response.WriteFile("~/Views/login.html");
                Response.End();
            }
            else if (path == "/register")
            {
                Response.ContentType = "text/html";
                Response.WriteFile("~/Views/register.html");
                Response.End();
            }
            else if (path.StartsWith("/api/"))
            {
                HandleApi(path);
            }
            else
            {
                Response.StatusCode = 404;
                Response.Write("404 - Not Found");
                Response.End();
            }
        }

        private void HandleApi(string path)
        {
            Response.ContentType = "application/json";

            if (path == "/api/login")
            {
                // TODO: call UserService.Authenticate() and return JSON
                Response.Write("{\"status\":\"ok\",\"message\":\"login endpoint\"}");
            }
            else if (path == "/api/register")
            {
                // TODO: call UserService.Register() and return JSON
                Response.Write("{\"status\":\"ok\",\"message\":\"register endpoint\"}");
            }
            else
            {
                Response.StatusCode = 404;
                Response.Write("{\"status\":\"error\",\"message\":\"Unknown API route\"}");
            }

            Response.End();
        }
    }
}
