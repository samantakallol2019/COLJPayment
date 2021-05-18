using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;
using COLJFunction.Configuration;
using CyberSource.Api;
using CyberSource.Model;

namespace COLJFunction
{
    public static class COLJFunction
    {
        [FunctionName("COLJFunction")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequestMessage req, ILogger log)
        {
            try
            {
                //JWT Generation
                var configDictionary = new Configuration.Configuration().GetConfiguration();
                var clientConfig = new CyberSource.Client.Configuration(merchConfigDictObj: configDictionary);
                var apiInstance = new KeyGenerationApi(clientConfig);
                var requestObj = new GeneratePublicKeyRequest("RsaOaep256", "https://pcidssdevapigateway.azure-api.net"); //https://pcidssdevapigateway.azure-api.net/TestPaymentConnectorApp/COLJFunction?Subscription-Key=426c014c7b6148dd883d6c8b04ddc595
                var result = apiInstance.GeneratePublicKey("JWT", requestObj);
                Console.WriteLine(result);
                Console.WriteLine(result.KeyId);


                // Call Your  API 
                HttpClient newClient = new HttpClient();
                HttpRequestMessage newRequest = new HttpRequestMessage(HttpMethod.Post, "https://appserviceenv.azurewebsites.net/colj");
                //HttpRequestMessage newRequest = new HttpRequestMessage(HttpMethod.Post, "http://localhost:65309/colj");   
                newRequest.Headers.Add("jwt", result.KeyId + Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(string.Format(""))));

                //Read Server Response
                HttpResponseMessage response = await newClient.SendAsync(newRequest);

                //Return view of respective application [colj, inflow etc] 
                return response;
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }
    }
    }

