using Aliencube.AzureFunctions.Extensions.OpenApi;
using Aliencube.AzureFunctions.Extensions.OpenApi.Abstractions;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Extensions.DependencyInjection;
using SwagerTestFunctionApp.Common.Configurations;
using SwagerTestFunctionApp.Common.Functions;
using SwaggerFunctionApp.Api;
using System;
using System.Collections.Generic;
using System.Text;

[assembly: WebJobsStartup(typeof(StartUp))]
namespace SwaggerFunctionApp.Api
{
    class StartUp : IWebJobsStartup
    {
        /// <summary>
        /// Configures <see cref="IWebJobsBuilder"/> and prepares dependencies.
        /// </summary>
        /// <param name="builder"><see cref="IWebJobsBuilder"/> instance.</param>
        public void Configure(IWebJobsBuilder builder)
        {
            builder.Services.AddSingleton<AppSettings>();

            builder.Services.AddTransient<IDocumentHelper, DocumentHelper>();
            builder.Services.AddTransient<IDocument, Document>();
            builder.Services.AddTransient<ISwaggerUI, SwaggerUI>();

            builder.Services.AddTransient<IRenderSwaggerUIFunction, RenderSwaggerUIFunction>();
            builder.Services.AddTransient<IRenderOpenApiDocumentFunction, RenderOpenApiDocumentFunction>();
        }
    }
}
