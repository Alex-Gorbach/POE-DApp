using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using POE.BLL.Interfaces;
using POE.WEB.Nethereum;
using System.Security.Cryptography;
using System;
using Microsoft.AspNet.Identity;
using System.IO;

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
                var contractWorcker = new Contract_work();
                var owner = UserService.GetAddress(id);
                contractWorcker.DeployContract(owner[0], password, result);

                return View("SuccessUpload");
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

        
        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase upload,string password)
        {
            if (upload != null)
            {
                //// получаем имя файла
                //string fileName = System.IO.Path.GetFileName(upload.FileName);
                //// сохраняем файл в папку Files в проекте
                //upload.SaveAs(Server.MapPath("~/Files/" + fileName));
                string id = User.Identity.GetUserId();
                var contractWorcker = new Contract_work();
                var owner = UserService.GetAddress(id);
                contractWorcker.DeployContract("0x0278168ff9757d848cec9d3bd0bc73d4093bd708", "452e7f5374a4574976dbeb7b6b24b201e243176cf24210337addf05ddd29ff2c", "7838F0C4E33740271E729BE1BC5B8176");
                return View("SuccessUpload");
            }
            return RedirectToAction("Index");
        }
    }
}