using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using POE.BLL.Interfaces;

namespace UserStore.Controllers
{
    public class HomeController : Controller
    {
        private IUserService UserService
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<IUserService>();
            }
        }

        public ActionResult Index()
        {
            return View();
        }
       
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
    }
}