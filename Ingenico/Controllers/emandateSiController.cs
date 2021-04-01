using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Ingenico.Models;

namespace Ingenico.Controllers
{
    public class emandateSiController : Controller
    {
        // GET: emandateSi
        public ActionResult mandateVerification()
        {
            return View();
        }
        [HttpPost]
        public ActionResult mandateVerification(FormCollection fc)
        {
            string path = Server.MapPath("~/App_Data/");

            string json = "";


            using (StreamReader r = new StreamReader(path + "output.json"))
            {
                json = r.ReadToEnd();

                r.Close();

            }
            JObject config_data = JObject.Parse(json);
            DateTime day = DateTime.Parse(fc["date"].ToString());

            var data = new
            {
                merchant = new {
                    identifier = config_data["merchantCode"].ToString()
                },
                payment = new {
                    instruction = new { },
                },
                transaction = new {
                    deviceIdentifier = "S",
                    type = fc["modeOfVerification"],
                    currency = config_data["currency"],
                    identifier = fc["merchantTxnId"],
                    dateTime = day.ToString("dd-M-yyyy"),
                    subType = "002",
                    requestType = "TSI"
                },
                consumer = new {
                    identifier = fc["consumerTxnId"]
                }
            };

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://www.paynimo.com/api/paynimoV2.req");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.PostAsJsonAsync("https://www.paynimo.com/api/paynimoV2.req", data).Result;
            var respStr = response.Content.ReadAsStringAsync();

            data = null;
            JObject dual_verification_result = JObject.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(respStr));
            var jsonData = JObject.Parse(dual_verification_result["Result"].ToString()).Children();

            List<JToken> tokens = jsonData.Children().ToList();
            ViewBag.tokens = tokens;

            return View();
        }

        public ActionResult transactionVerification()
        {
            return View();
        }
        [HttpPost]
        public ActionResult transactionVerification(FormCollection fc)
        {

            string path = Server.MapPath("~/App_Data/");

            string json = "";


            using (StreamReader r = new StreamReader(path + "output.json"))
            {
                json = r.ReadToEnd();

                r.Close();

            }
            JObject config_data = JObject.Parse(json);
            DateTime day = DateTime.Parse(fc["date"].ToString());

            var data = new
            {
                merchant = new
                {
                    identifier = config_data["merchantCode"].ToString()
                },
                payment = new
                {
                    instruction = new { },
                },
                transaction = new
                {
                    deviceIdentifier = "S",
                    type = fc["modeOfVerification"].ToString(),
                    currency = config_data["currency"],
                    identifier = fc["merchantTxnId"],
                    dateTime = day.ToString("dd-M-yyyy"),
                    subType = "004",
                    requestType = "TSI"
                }
            };

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://www.paynimo.com/api/paynimoV2.req");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.PostAsJsonAsync("https://www.paynimo.com/api/paynimoV2.req", data).Result;
            var respStr = response.Content.ReadAsStringAsync();

            data = null;
            JObject dual_verification_result = JObject.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(respStr));
            var jsonData = JObject.Parse(dual_verification_result["Result"].ToString()).Children();

            List<JToken> tokens = jsonData.Children().ToList();
            ViewBag.tokens = tokens;

            return View();
        }

        public ActionResult transactionScheduling()
        {
            return View();
        }
        [HttpPost]


        public ActionResult transactionScheduling(FormCollection fc)
        {
            string path = Server.MapPath("~/App_Data/");
            string json = "";
            using (StreamReader r = new StreamReader(path + "output.json"))
            {
                json = r.ReadToEnd();
                r.Close();
            }
            JObject config_data = JObject.Parse(json);
            DateTime day = DateTime.Parse(fc["date"].ToString()); string tranId = GenerateRandomString(12);
            //ViewBag.tranId = tranId;
            var data = new
            {
                merchant = new
                {
                    identifier = config_data["merchantCode"].ToString()
                },
                payment = new
                {
                    instrument = new
                    {
                        identifier = config_data["merchantSchemeCode"].ToString()
                    },
                    instruction = new
                    {
                        amount = fc["amount"],
                        endDateTime = day.ToString("ddMMyyyy"),
                        identifier = fc["mandateRegId"]
                    }
                },
                transaction = new
                {
                    deviceIdentifier = "S",
                    type = fc["modeOfTransaction"].ToString(),
                    currency = config_data["currency"],
                    //identifier = fc["merchantTxnId"],
                    identifier = tranId,
                    subType = "003",
                    requestType = "TSI"
                }
            };
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://www.paynimo.com/api/paynimoV2.req");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.PostAsJsonAsync("https://www.paynimo.com/api/paynimoV2.req", data).Result;
            var respStr = response.Content.ReadAsStringAsync();
            data = null;
            JObject dual_verification_result = JObject.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(respStr));
            var jsonData = JObject.Parse(dual_verification_result["Result"].ToString()).Children();
            List<JToken> tokens = jsonData.Children().ToList();
            ViewBag.tokens = tokens;
            return View();
        }


        public string GenerateRandomString(int size)
        {            //Guid g = Guid.NewGuid();            //string random1 = g.ToString();
                     //random1 = random1.Replace("-", "");
                     //var builder = random1.Substring(0, size);            
            var temp = Guid.NewGuid().ToString().Replace("-", string.Empty);
            var barcode = Regex.Replace(temp, "[a-zA-Z]", string.Empty).Substring(0, 10); return barcode.ToString();
        }



        public ActionResult stopPayment()
        {
            return View();
        }
        [HttpPost]
        public ActionResult stopPayment(FormCollection fc)
        {
            string path = Server.MapPath("~/App_Data/");
            string json = "";
            using (StreamReader r = new StreamReader(path + "output.json"))
            {
                json = r.ReadToEnd();
                r.Close();
            }
            JObject config_data = JObject.Parse(json);
            //    DateTime day = DateTime.Parse(fc["date"].ToString()); 
            string tranId = GenerateRandomString(12);

            var jsonStr = new
            {
                merchant = new
                {
                    webhookEndpointURL = "",
                    responseType = "",
                    responseEndpointURL = "",
                    description = "",
                    identifier = config_data["merchantCode"].ToString(),
                    webhookType = ""
                },
                cart = new
                {
                    item = new[]{ new {description= "",
                        providerIdentifier= "",
                        surchargeOrDiscountAmount= "",
                        amount= "",
                        comAmt= "",
                        sKU= "",
                        reference= "",
                        identifier= ""

                        } },
                    reference = "",
                    identifier = "",
                    description = "",
                    Amount = ""
                },
                payment = new
                {
                    method = new
                    {
                        token = "",
                        type = ""
                    },
                    instrument = new
                    {
                        expiry = new
                        {
                            year = "",
                            month = "",
                            dateTime = ""
                        },
                        provider = "",
                        iFSC = "",
                        holder = new
                        {
                            name = "",
                            address = new
                            {
                                country = "",
                                street = "",
                                state = "",
                                city = "",
                                zipCode = "",
                                county = ""
                            }
                        },
                        bIC = "",
                        type = "",
                        action = "",
                        mICR = "",
                        verificationCode = "",
                        iBAN = "",
                        processor = "",
                        issuance = new
                        {
                            year = "",
                            month = "",
                            dateTime = ""
                        },
                        alias = "",
                        identifier = "test",
                        token = "",
                        authentication = new
                        {
                            token = "",
                            type = "",
                            subType = ""
                        },
                        subType = "",
                        issuer = "",
                        acquirer = ""
                    },
                    instruction = new
                    {
                        occurrence = "",
                        amount = "",
                        frequency = "",
                        type = "",
                        description = "",
                        action = "",
                        limit = "",
                        endDateTime = "",
                        identifier = "",
                        reference = "",
                        startDateTime = "",
                        validity = ""
                    }
                },
                transaction = new
                {
                    deviceIdentifier = "S",
                    smsSending = "",
                    amount = "",
                    forced3DSCall = "",
                    type = "001",
                    description = "",
                    currency = config_data["currency"],
                    isRegistration = "",
                    identifier = tranId,
                    dateTime = "",
                    token = fc["txnId"].ToString(),
                    securityToken = "",
                    subType = "006",
                    requestType = "TSI",
                    reference = "",
                    merchantInitiated = "",
                    merchantRefNo = ""
                },
                consumer = new
                {
                    mobileNumber = "",
                    emailID = "",
                    identifier = "",
                    accountNo = ""
                }
            };

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://www.paynimo.com/api/paynimoV2.req");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.PostAsJsonAsync("https://www.paynimo.com/api/paynimoV2.req", jsonStr).Result;
            var respStr = response.Content.ReadAsStringAsync();
            jsonStr = null;
            JObject dual_verification_result = JObject.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(respStr));
            var jsonData = JObject.Parse(dual_verification_result["Result"].ToString()).Children();
            List<JToken> tokens = jsonData.Children().ToList();
            ViewBag.tokens = tokens;
            return View();
        }
        public string jsoNStpPayment()
        {
            var jsonStr = new
            {
                merchant = new
                {
                    webhookEndpointURL = "",
                    responseType = "",
                    responseEndpointURL = "",
                    description = "",
                    identifier = "L95279",
                    webhookType = ""
                },
                cart = new
                {
                    item = new[]{ new {description= "",
                        providerIdentifier= "",
                        surchargeOrDiscountAmount= "",
                        amount= "",
                        comAmt= "",
                        sKU= "",
                        reference= "",
                        identifier= ""

                        } },
                    reference = "",
                    identifier = "",
                    description = "",
                    Amount = ""
                },
                payment = new
                {
                    method = new
                    {
                        token = "",
                        type = ""
                    },
                    instrument = new
                    {
                        expiry = new
                        {
                            year = "",
                            month = "",
                            dateTime = ""
                        },
                        provider = "",
                        iFSC = "",
                        holder = new
                        {
                            name = "",
                            address = new
                            {
                                country = "",
                                street = "",
                                state = "",
                                city = "",
                                zipCode = "",
                                county = ""
                            }
                        },
                        bIC = "",
                        type = "",
                        action = "",
                        mICR = "",
                        verificationCode = "",
                        iBAN = "",
                        processor = "",
                        issuance = new
                        {
                            year = "",
                            month = "",
                            dateTime = ""
                        },
                        alias = "",
                        identifier = "test",
                        token = "",
                        authentication = new
                        {
                            token = "",
                            type = "",
                            subType = ""
                        },
                        subType = "",
                        issuer = "",
                        acquirer = ""
                    },
                    instruction = new
                    {
                        occurrence = "",
                        amount = "11",
                        frequency = "",
                        type = "",
                        description = "",
                        action = "",
                        limit = "",
                        endDateTime = "",
                        identifier = "",
                        reference = "",
                        startDateTime = "",
                        validity = ""
                    }
                },
                transaction = new
                {
                    deviceIdentifier = "S",
                    smsSending = "",
                    amount = "",
                    forced3DSCall = "",
                    type = "001",
                    description = "",
                    currency = "INR",
                    isRegistration = "",
                    identifier = "m14",
                    dateTime = "",
                    token = "700000000001",
                    securityToken = "",
                    subType = "006",
                    requestType = "TSI",
                    reference = "",
                    merchantInitiated = "",
                    merchantRefNo = ""
                },
                consumer = new
                {
                    mobileNumber = "",
                    emailID = "",
                    identifier = "",
                    accountNo = ""
                }
            };
            return "test";
        }


        public ActionResult mandateDeactivation()
        {
            return View();
        }
        [HttpPost]
        public ActionResult mandateDeactivation(FormCollection fc)
        {
            string path = Server.MapPath("~/App_Data/");
            string json = "";
            using (StreamReader r = new StreamReader(path + "output.json"))
            {
                json = r.ReadToEnd();
                r.Close();
            }
            JObject config_data = JObject.Parse(json);
            string tranId = GenerateRandomString(12);
            var data = new Root();
            data.merchant.identifier = config_data["merchantCode"].ToString();
            //data.merchant.identifier = "L95279";
            data.transaction.deviceIdentifier = "S";
            data.transaction.type = fc["modeOfTransaction"];
           // data.transaction.type = "001";
            data.transaction.currency = config_data["currency"].ToString();
            data.transaction.identifier = tranId;
            data.transaction.token = fc["mandateRegId"];
            //data.transaction.token = "761582872";
            data.transaction.subType = "005";
            data.transaction.requestType = "TSI";
            data.cart.item.Add(new Item());
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://www.paynimo.com/api/paynimoV2.req");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.PostAsJsonAsync("https://www.paynimo.com/api/paynimoV2.req", data).Result;
            var respStr = response.Content.ReadAsStringAsync();
            data = null;
            JObject dual_verification_result = JObject.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(respStr));
            var jsonData = JObject.Parse(dual_verification_result["Result"].ToString()).Children();
            List<JToken> tokens = jsonData.Children().ToList();
            ViewBag.tokens = tokens;
            return View();
        }
    }
}