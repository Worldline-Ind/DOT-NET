using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Text.RegularExpressions;
using Ingenico.CORS;
using Newtonsoft.Json.Linq;

namespace Ingenico.Controllers
{
    [AllowCrossSite]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            try
            {
                string path = Server.MapPath("~/App_Data/");
                string tranId = GenerateRandomString(12);
                ViewBag.tranId = tranId;

                ViewBag.debitStartDate = DateTime.Now.ToString("yyyy-MM-dd");
                int year = Convert.ToInt32(DateTime.Now.Year.ToString());
                DateTime date = DateTime.Now;
                var enddate = date.AddYears(30);
                ViewBag.debitEndDate = enddate.ToString("yyyy-MM-dd");
                //dd-MM-yyyy
                using (StreamReader r = new StreamReader(path + "output.json"))
                {
                    string json = r.ReadToEnd();


                    ViewBag.config_data = json;
                    var jsonData = JObject.Parse(json).Children();
                    List<JToken> tokens = jsonData.Children().ToList();
                    if (Convert.ToBoolean(tokens[25])==true)
                    {
                        if (Convert.ToBoolean(tokens[34]) == true)
                        {
                            ViewBag.enbSi = Convert.ToBoolean(tokens[25]);
                        }
                        else
                        {
                            ViewBag.enbSi = false;
                        }
                       
                    }
                    else
                    {
                        ViewBag.enbSi = false;
                    }

                }
            }
            catch (Exception ex)
            {

               // throw;
            }
           


            return View();
        }
        [HttpPost]
        public ActionResult Index(FormCollection formCollection)
        {
           
            foreach (var key in formCollection.AllKeys)
            {
                var value = formCollection[key];
            }
            string dataString = formCollection["mrctCode"] + "|" + formCollection["txn_id"] + "|" + formCollection["amount"] + "|" + formCollection["accNo"] + "|"
             + formCollection["custID"] + "|" + formCollection["mobNo"] + "|" + formCollection["email"] + "|" + formCollection["debitStartDate"] + "|" + formCollection["debitEndDate"]
             + "|" + formCollection["maxAmount"] + "|" + formCollection["amountType"] + "|" + formCollection["frequency"] + "|" + formCollection["cardNumber"] + "|"
             + formCollection["expMonth"] + "|" + formCollection["expYear"] + "|" + formCollection["cvvCode"] + "|" + formCollection["SALT"];

         
            return View();

           
        }

     
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public JsonResult GenerateSHA512String(string inputString)
        {
          

            using (SHA512 sha512Hash = SHA512.Create())
            {
                //From String to byte array
                byte[] sourceBytes = Encoding.UTF8.GetBytes(inputString);
                byte[] hashBytes = sha512Hash.ComputeHash(sourceBytes);
                string hash = BitConverter.ToString(hashBytes).Replace("-", String.Empty);

                System.Security.Cryptography.SHA512Managed sha512 = new System.Security.Cryptography.SHA512Managed();

                Byte[] EncryptedSHA512 = sha512.ComputeHash(System.Text.Encoding.UTF8.GetBytes(hash));

                sha512.Clear();

                var bts= Convert.ToBase64String(EncryptedSHA512);

                return Json(hash, JsonRequestBehavior.AllowGet);
            }
        }
        public  string GenerateRandomString(int size)
        {

            //Guid g = Guid.NewGuid();

            //string random1 = g.ToString();
            //random1 = random1.Replace("-", "");
            //var builder = random1.Substring(0, size);

            var temp = Guid.NewGuid().ToString().Replace("-", string.Empty);
            var barcode = Regex.Replace(temp, "[a-zA-Z]", string.Empty).Substring(0, 10);

            return barcode.ToString();
        }
        private static string GetStringFromHash(byte[] hash)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                result.Append(hash[i].ToString("X2"));
            }
            return result.ToString();
        }
    }
}