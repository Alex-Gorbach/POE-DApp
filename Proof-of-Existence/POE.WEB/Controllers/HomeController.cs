using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using POE.BLL.Interfaces;
using POE.WEB.Nethereum;
using System.Security.Cryptography;
using System;
using Microsoft.AspNet.Identity;
using System.IO;
using Nethereum.Geth;
using Nethereum.Web3;

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


        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file,string password)
        {

            if (file.ContentLength > 0)
            {
                byte[] imageData = null;

                using (var binaryReader = new BinaryReader(file.InputStream))
                {
                    imageData = binaryReader.ReadBytes(file.ContentLength);
                }
                MD5 md5 = new MD5CryptoServiceProvider();
                byte[] hashenc = md5.ComputeHash(imageData);
                string result = "";
                foreach (var b in hashenc)
                {
                    result += b.ToString("x2");
                }

                string id = User.Identity.GetUserId();
                var owner = UserService.GetAddress(id);
                var contractService = new ContractService(owner[0], password, result);

                contractService.SetFileHash();
               
            }

            return RedirectToAction("Index");
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