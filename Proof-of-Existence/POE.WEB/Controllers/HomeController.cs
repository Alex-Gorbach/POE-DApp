using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using POE.BLL.Interfaces;
using POE.WEB.Nethereum;
using System.IO;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

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
        public async Task<ActionResult> Index(HttpPostedFileBase file1,string privateKeyUpload)
        {
            string tranactionHash="";
            if (file1 != null)
            {
                byte[] imageData = null;

                using (var binaryReader = new BinaryReader(file1.InputStream))
                {
                    imageData = binaryReader.ReadBytes(file1.ContentLength);
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
                var contractService = new ContractService(owner[0], privateKeyUpload, result);
                tranactionHash=await contractService.SetFileHash();
                ViewData["Message"] ="Transaction hash: "+ tranactionHash;


            }
            else
            {
                ViewData["Message"] = "Please, Select a file.";
            }

            return View("Index");
        }

        [HttpPost]
        public async Task<ActionResult> Check(HttpPostedFileBase file2,string privateKeyCheck)
        {
            string checkResult="";
            if (file2 !=null)
            {
                byte[] imageData = null;
                
                using (var binaryReader = new BinaryReader(file2.InputStream))
                {
                    imageData = binaryReader.ReadBytes(file2.ContentLength);
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
                var contractService = new ContractService(owner[0], privateKeyCheck, result);
                checkResult = await contractService.GetFileHash();
                if (checkResult == "") { 
                    checkResult = "A document with this hash was not found.";
                ViewData["Message"] = checkResult;
                }
                else
                {
                    ViewData["Message"] ="File existe. Owner: "+ checkResult;
                }

            }
            else
            {
                ViewData["Message"] = "Please, select a file.";
                return View("Index");
            }
            

            return View("SuccessUpload");
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