using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using System.Net;
namespace Ingenico.Controllers
{
    public class ResponseController : Controller
    {
        // GET: Response
        [HttpPost]
        public ActionResult responseHandler(FormCollection formCollection)
        {
            try
            {
                foreach (var key in formCollection.AllKeys)
                {
                    var value = formCollection[key];
                }

                string path = Server.MapPath("~/App_Data/");

                string json = "";


                using (StreamReader r = new StreamReader(path + "output.json"))
                {
                    json = r.ReadToEnd();

                    r.Close();

                }
                JObject config_data = JObject.Parse(json);
                var data = formCollection["msg"].ToString().Split('|');
                if (data==null )
                {//|| data[1].ToString()== "User Aborted"
                    ViewBag.abrt = true;
                    return Redirect(ControllerContext.HttpContext.Request.UrlReferrer.ToString());
                }
                ViewBag.online_transaction_msg = data;
                if (data[0] == "0300")
                {
                    //var request_str = "{ :merchant => { :identifier => " + config_data["merchantCode"].ToString() + "} ," +
                    //                " :transaction => { :deviceIdentifier => 'S', " +
                    //                     ":currency => " + config_data["currency"] + "," +
                    //                     ":dateTime => " + string.Format("{0:d/M/yyyy", data[8]) + "," +
                    //                     ":token => " + data[5].ToString() + "," +
                    //                     ":requestType => 'S'" +
                    //                     "}" +
                    //                    "}";
                    ViewBag.abrt = false;

                    var strJ = new
                    {
                        merchant = new
                        {
                            identifier = config_data["merchantCode"].ToString()
                        },
                        transaction = new
                        {
                            deviceIdentifier = "S",
                            currency = config_data["currency"],
                            dateTime = string.Format("{0:d/M/yyyy}", data[8].ToString()),
                            token = data[5].ToString(),
                            requestType = "S"
                        }
                    };
                    //     var request_str = Newtonsoft.Json.JsonConvert.SerializeObject(strJ);

                    //  JObject request_data = JObject.Parse(request_str);
                    //using (var client = new HttpClient())
                    //{
                    //    client.BaseAddress = new Uri("https://www.paynimo.com/api/paynimoV2.req");

                    //    //HTTP POST
                    //    var postTask = client.PostAsync("https://www.paynimo.com/api/paynimoV2.req", request_data);
                    //    postTask.Wait();

                    //    var result = postTask.Result;
                    //    if (result.IsSuccessStatusCode)
                    //    {
                    //        return RedirectToAction("Index");
                    //    }
                    //}


                    HttpClient client = new HttpClient();
                    client.BaseAddress = new Uri("https://www.paynimo.com/api/paynimoV2.req");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = client.PostAsJsonAsync("https://www.paynimo.com/api/paynimoV2.req", strJ).Result;
                    var a = response.Content.ReadAsStringAsync();

                    JObject dual_verification_result = JObject.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(a));
                    var jsonData = JObject.Parse(dual_verification_result["Result"].ToString()).Children();

                    List<JToken> tokens = jsonData.Children().ToList();

                    var jsonData1 = JObject.Parse(tokens[6].ToString()).Children();
                    List<JToken> tokens1 = jsonData.Children().ToList();
                    ViewBag.dual_verification_result = dual_verification_result;
                    ViewBag.a = a;
                    ViewBag.jsonData = jsonData;
                    ViewBag.tokens = tokens;
                    ViewBag.paramsData = formCollection["msg"];

                    // return response;
                }

            }
            catch (Exception ex)
            {

                //throw;
            }
           

            
                return View();
        }
    }
}