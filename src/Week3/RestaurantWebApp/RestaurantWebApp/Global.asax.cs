using System;
using System.Data.SqlClient;
using System.Web;
using Newtonsoft.Json;
using SimpleWebApp.Controllers;

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

                using (var conn = new SqlConnection(connStr))
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
            string path = Request.Path.ToLower().TrimEnd('/'); // normalize path

            // === Static files (css, js, images, favicon) ===
            if (path.EndsWith(".css", StringComparison.OrdinalIgnoreCase) ||
                path.EndsWith(".js", StringComparison.OrdinalIgnoreCase) ||
                path.EndsWith(".png", StringComparison.OrdinalIgnoreCase) ||
                path.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) ||
                path.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase) ||
                path.EndsWith(".gif", StringComparison.OrdinalIgnoreCase) ||
                path.EndsWith(".ico", StringComparison.OrdinalIgnoreCase) ||
                path.EndsWith(".svg", StringComparison.OrdinalIgnoreCase) ||
                path.EndsWith(".webp", StringComparison.OrdinalIgnoreCase) ||
                path.EndsWith(".avif", StringComparison.OrdinalIgnoreCase))
            {
                return; // Let IIS/ASP.NET serve them directly
            }


            // === Let ASP.NET handle .aspx pages directly ===
            if (path.EndsWith(".aspx"))
            {
                return;
            }

            // === Virtual routes ===
            if (path == "" || path == "/" || path == "/index")
            {
                Context.Server.Transfer("~/Views/index.aspx");
            }
            else if (path == "/login")
            {
                Context.Server.Transfer("~/Views/login.aspx"); 
            }
            else if (path == "/register")
            {
                Context.Server.Transfer("~/Views/register.aspx"); 
            }
            // === API routing ===
            else if (path.StartsWith("/api/"))
            {
                RouteApi(path, Context);
            }
            // === Fallback ===
            else
            {
                JsonError(Response, 404, "Not Found");
            }
        }

        private void ServeStatic(string filePath)
        {
            Response.ContentType = "text/html";
            Response.WriteFile(filePath);
            Response.End();
        }

        private void RouteApi(string path, HttpContext context)
        {
            var segments = path.Trim('/').Split('/');
            if (segments.Length < 2)
            {
                JsonError(Response, 404, "Invalid API route");
                return;
            }

            string controllerName = segments[1];
            IController controller;

            switch (controllerName)
            {
                case "user":
                case "admin":
                case "register":
                case "profile":
                    controller = new UserController();
                    break;
                case "auth":
                case "login":
                case "logout":
                    controller = new AuthController();
                    break;
                default:
                    controller = null;
                    break;
            }

            if (controller != null)
            {
                controller.Handle(context);
            }
            else
            {
                JsonError(Response, 404, "Controller Not Found");
            }
        }


        private void JsonError(HttpResponse response, int statusCode, string message)
        {
            response.StatusCode = statusCode;
            response.ContentType = "application/json";
            response.Write(JsonConvert.SerializeObject(new { error = message }));
            response.End();
        }
    }
}
