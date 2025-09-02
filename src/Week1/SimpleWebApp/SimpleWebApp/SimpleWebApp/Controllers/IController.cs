using System.Web;

namespace SimpleWebApp.Controllers
{
    public interface IController
    {
        void Handle(HttpContext context);
    }
}