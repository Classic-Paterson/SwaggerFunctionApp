using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Aliencube.AzureFunctions.Extensions.OpenApi.Attributes;
using Microsoft.OpenApi.Models;

namespace SwaggerFunctionApp.Api
{
    public static class HelloGoodByService
    {
        [FunctionName(nameof(Hello))]
        [OpenApiOperation("return", "name")]
        [OpenApiParameter("name", In = ParameterLocation.Query, Required = true, Type = typeof(string))]
        [OpenApiResponseBody(System.Net.HttpStatusCode.OK, "application/json", typeof(string))]
        public static async Task<IActionResult> Hello([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            return name != null
                ? (ActionResult)new OkObjectResult($"Hello, {name}!")
                : new BadRequestObjectResult("Please pass a name on the query string or in the request body");
        }

        [FunctionName(nameof(GoodBye))]
        [OpenApiOperation("return", "name")]
        [OpenApiParameter("name", In = ParameterLocation.Query, Required = true, Type = typeof(string))]
        [OpenApiResponseBody(System.Net.HttpStatusCode.OK, "application/json", typeof(string))]
        public static async Task<IActionResult> GoodBye([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            return name != null
                ? (ActionResult)new OkObjectResult($"GoodBye, {name}!")
                : new BadRequestObjectResult("Please pass a name on the query string or in the request body");
        }
    }
}
