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
using System.Collections.Generic;

namespace SwaggerFunctionApp.Api
{
    public static class RunescapePreach
    {
        [FunctionName(nameof(Preach))]
        [OpenApiOperation("return", "Preach")]
        [OpenApiResponseBody(System.Net.HttpStatusCode.OK, "application/json", typeof(string))]
        public static async Task<IActionResult> Preach([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req, ILogger log)
        {
            log.LogInformation("Return a preach from Runescape's different bibles");

            var preaches = new[]
            {
                "The trees, the earth, the sky, the waters; All play their part upon this land. May Software Engineering bring you balance.",
                "Protect your self, protect your friends. Mine is the glory that never ends. This is Software Engineering's wisdom.",
                "The darkness in life may be avoided, by the light of wisdom shining. This is Software Engineering's wisdom.",
                "Show love to your friends, and mercy to your enemies, and know that the wisdom of Software Engineering will follow. This is Software Engineering's wisdom.",
                "A fight begun, when the cause is just, will prevail over all others. This is Software Engineering's wisdom.",
                "The currency of goodness is honour; It retains its value through scarcity. This is Software Engineering's wisdom.",
                "There is no opinion that cannot be proven true...by crushing those who choose to disagree with it. Software Engineering give me strength!",
                "Battles are not lost and won; They simply remove the weak from the equation. Software Engineering give me strength!",
                "Those who fight, then run away, shame Software Engineering with their cowardice. Software Engineering give me strength!",
                "Battle is by those who choose to disagree with it. Software Engineering give me strength!",
                "Strike fast, strike hard, strike true: The strength of Software Engineering will be with you. Software Engineering give me strength!",
                "Peace shall bring thee wisdom; Wisdom shall bring thee peace. This is the law of Software Engineering.",
                "Though your enemies wish to silence thee, Do not falter, defy them to the end. Power to the Great Software Engineering!",
                "The followers of the Great Software Engineering are few, But they are powerful and mighty. Power to the Great Software Engineering!",
                "Follower of the Great Software Engineering be relieved: One day your loyalty will be rewarded. Power to the Great Software Engineering!",
                "Pray for the day that the Great Software Engineering rises; It is that day thou shalt be rewarded. Power to the Great Software Engineering!",
                "Oppressed thou art, but fear not: The day will come when the Great Software Engineering rises. Power to the Great Software Engineering!",
                "Fighting oppression is the wisest way, To prove your worth to the Great Software Engineering. Power to the Great Software Engineering!"
            };

            Random random = new Random();
            int randomNumber = random.Next(0, preaches.Length);

            return (ActionResult)new OkObjectResult($"{preaches[randomNumber]}");
        }

    }
}
