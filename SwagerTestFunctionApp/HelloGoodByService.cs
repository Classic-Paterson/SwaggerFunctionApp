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
        [OpenApiOperation("return", "Pleasantries")]
        [OpenApiParameter("name", In = ParameterLocation.Query, Required = true, Type = typeof(string))]
        [OpenApiResponseBody(System.Net.HttpStatusCode.OK, "application/json", typeof(string))]
        public static async Task<IActionResult> Hello([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req, ILogger log)
        {
            log.LogInformation("Returns a warm greeting to the user.");

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            return name != null
                ? (ActionResult)new OkObjectResult($"Hello, {name}!")
                : new BadRequestObjectResult("Please pass a name on the query string or in the request body");
        }

        [FunctionName(nameof(GoodBye))]
        [OpenApiOperation("return", "Pleasantries")]
        [OpenApiParameter("name", In = ParameterLocation.Query, Required = true, Type = typeof(string))]
        //[OpenApiParameter("date", In = ParameterLocation.Query, Required = true, Type = typeof(string))]
        [OpenApiResponseBody(System.Net.HttpStatusCode.OK, "application/json", typeof(string))]
        public static async Task<IActionResult> GoodBye([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req, ILogger log)
        {
            log.LogInformation("Returns a humble goodbye to the user.");

            string name = req.Query["name"];
            //string date = req.Query["date"];

            //string datetime = DateTime.Parse(date).ToString("dddd, dd MMMM yyyy");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            string message = $"Dear {name},\n\nAs you know by now, I am leaving Datacom. \n\nI want to take a few minutes today to convey my thoughts in being part of your team for the last few years.\n\nI have been extremely satisfied with my time at Datacom, working under your guidance has been a learning and an enjoyable experience. I thank you for your support and encouragement during these years. However, I feel that it is time for me to move on to new opportunities. This decision was not an easy one and it took a lot of consideration. I think this decision is in the best interests towards fulfilling my career goals. I want to do my best in completing my existing responsibilities and then ensuring a smooth transition.\n\nI would like to thank you again, {name}, for the help and guidance during all these years of my employment, and would like to extend my best wishes to the entire group.\n\nMy personal contacts are:\nEmail(reece@patersonprojects.com)\nContact phone number(021 920 443).\n\nPlease feel free to contact me even later on, in either a personal or professional manner.\n\nSincerely,\nReece Paterson";

            return name != null
                ? (ActionResult)new OkObjectResult(message)
                : new BadRequestObjectResult("Please pass a name on the query string or in the request body");
        }
    }
}
