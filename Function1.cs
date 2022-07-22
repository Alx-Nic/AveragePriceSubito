using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
//using Newtonsoft.Json;
using PrezziSubito.Models.Request;
using Newtonsoft.Json;
using HtmlAgilityPack;
using PrezziSubito.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace PrezziSubito
{
    public static class Function1
    {
        [FunctionName("AveragePriceSubito")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            //string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            //Urls data = (Urls)JsonConvert.DeserializeObject(requestBody);

            string json = await req.ReadAsStringAsync();

            var calculateRequest = JsonConvert.DeserializeObject<CalculateRequest>(json);

            int totalAdds = 0;
            int page = 0;
            List<decimal> prices = new List<decimal>();

            foreach (var url in calculateRequest.Urls)
            {
                var webPage = PageHelpers.LoadDocument(url);
                
                page++;

                if (page == 1) totalAdds = PageHelpers.CalculateTotalAds(webPage);

                var foundPrices = PageHelpers.FindPrices(webPage);

                foreach (var price in foundPrices)
                {
                    prices.Add(price);
                }

            }

            string responseMessage = ($"We have used {prices.Count} products to calculate an average price of {prices.Average():c} from a total of {totalAdds} ads.");

            Console.WriteLine(responseMessage);

            return new OkObjectResult(responseMessage);
        }
    }
}
