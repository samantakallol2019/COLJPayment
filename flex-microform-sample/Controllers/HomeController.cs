using CyberSource.Api;
using CyberSource.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;


namespace COLJPayment.Controllers
{
    public class HomeController : System.Web.Mvc.Controller
    {

        public System.Web.Mvc.ActionResult Checkout(string id)
        {
            /**
             * Generating Capture Context Request Payload
             * Defining Encryption Type = RsaOaep
             * Defining TargetOrigin = http://localhost:65309
             */
            //For Production
            try
            {
                ViewBag.Jwk = "123";
                var hdr = this.Request.Headers.GetValues("jwt");
                if (hdr != null)
                    ViewBag.Jwk = hdr[0];
            }
            catch (Exception e)
            {
                throw e;
            }
            if (id == "colj")
                return View("COLJ/Checkout");
            else if (id == "pws")
                return View("PWS/Checkout");
            else if (id == "inflow")
                return View("Inflow/Checkout");
            else
                return View("Inflow/Checkout");

        }

        public System.Web.Mvc.ActionResult Receipt()
        {
            #region
            //dynamic flexObj = Request.Params["flexResponse"];

            ///**
            // * Processing Authorization Request
            // * Code developed from CyberSource Rest Samples csharp
            // * https://github.com/CyberSource/cybersource-rest-samples-csharp
            // */

            //var processingInformationObj = new Ptsv2paymentsProcessingInformation() { CommerceIndicator = "internet" };

            //var clientReferenceInformationObj = new Ptsv2paymentsClientReferenceInformation { Code = "test_payment" };

            //var orderInformationObj = new Ptsv2paymentsOrderInformation();

            //var billToObj = new Ptsv2paymentsOrderInformationBillTo
            //{
            //    Country = "US",
            //    FirstName = "Amar",
            //    LastName = "Pawar",
            //    Address1 = "1 Market St",
            //    PostalCode = "94105",
            //    Locality = "San Francisco",
            //    AdministrativeArea = "CA",
            //    Email = "test@cybs.com"
            //};

            //orderInformationObj.BillTo = billToObj;

            //var amountDetailsObj = new Ptsv2paymentsOrderInformationAmountDetails
            //{
            //    TotalAmount = "20.21",
            //    Currency = "USD"
            //};

            //orderInformationObj.AmountDetails = amountDetailsObj;

            //// Passing Transient token

            //var transientTokenObj = new Ptsv2paymentsTokenInformation { TransientTokenJwt = flexObj };

            //var requestObj = new CreatePaymentRequest
            //{
            //    ProcessingInformation = processingInformationObj,
            //    ClientReferenceInformation = clientReferenceInformationObj,
            //    OrderInformation = orderInformationObj,
            //    TokenInformation = transientTokenObj
            //};


            //try
            //{
            //    var configDictionary = new Configuration().GetConfiguration();
            //    var clientConfig = new CyberSource.Client.Configuration(merchConfigDictObj: configDictionary);
            //    var apiInstance = new PaymentsApi(clientConfig);

            //    var result = apiInstance.CreatePayment(requestObj);

            //    Console.WriteLine(result);

            //    //Making response pretty & passing to page
            //    ViewBag.paymentResponse = result;
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine("Exception on calling the API: " + e.Message);
            //    return null;
            //}
            #endregion

            return View();
        }

        public System.Web.Mvc.ActionResult Token()
        {
            //dynamic flexObj = JValue.Parse(Request.Params["flexResponse"]);
            dynamic flexObj = Request.Params["flexResponse"];

            ViewBag.JWT = flexObj.Replace("\r\n", "").Replace("\"", "");


            return View();
        }

    }
}