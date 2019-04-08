﻿using Aliencube.AzureFunctions.Extensions.DependencyInjection.Triggers.Abstractions;
using Aliencube.AzureFunctions.Extensions.DependencyInjection.Extensions;
using Aliencube.AzureFunctions.Extensions.OpenApi.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using SwagerTestFunctionApp.Common.Functions;
using SwagerTestFunctionApp.Common.Functions.FunctionOptions;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SwaggerFunctionApp.Api
{
    public class OpenApiHttpTrigger : TriggerBase<ILogger>
    {
        private readonly IRenderOpenApiDocumentFunction _openApiDoc;
        private readonly IRenderSwaggerUIFunction _swaggerUi;

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenApiHttpTrigger"/> class.
        /// </summary>
        /// <param name="openApiDoc"><see cref="IRenderOpenApiDocumentFunction"/> instance.</param>
        /// <param name="swaggerUi"><see cref="IRenderSwaggerUIFunction"/> instance.</param>
        public OpenApiHttpTrigger(IRenderOpenApiDocumentFunction openApiDoc, IRenderSwaggerUIFunction swaggerUi)
        {
            this._openApiDoc = openApiDoc ?? throw new ArgumentNullException(nameof(openApiDoc));
            this._swaggerUi = swaggerUi ?? throw new ArgumentNullException(nameof(swaggerUi));
        }

        /// <summary>
        /// Invokes the HTTP trigger endpoint to get Open API document.
        /// </summary>
        /// <param name="req"><see cref="HttpRequest"/> instance.</param>
        /// <param name="extension">File extension representing the document format. This MUST be either "json" or "yaml".</param>
        /// <param name="log"><see cref="ILogger"/> instance.</param>
        /// <returns>Open API document in a format of either JSON or YAML.</returns>
        [FunctionName(nameof(RenderSwaggerDocument))]
        [OpenApiIgnore]
        public async Task<IActionResult> RenderSwaggerDocument(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "swagger.{extension}")] HttpRequest req,
            string extension,
            ILogger log)
        {
            RenderOpenApiDocumentFunctionOptions options = new RenderOpenApiDocumentFunctionOptions("v2", extension, Assembly.GetExecutingAssembly());
            var result = await this._openApiDoc
                                   .AddLogger(log)
                                   .InvokeAsync<HttpRequest, IActionResult>(req, options)
                                   .ConfigureAwait(false);

            return result;
        }

        /// <summary>
        /// Invokes the HTTP trigger endpoint to get Open API document.
        /// </summary>
        /// <param name="req"><see cref="HttpRequest"/> instance.</param>
        /// <param name="version">Open API document spec version. This MUST be either "v2" or "v3".</param>
        /// <param name="extension">File extension representing the document format. This MUST be either "json" or "yaml".</param>
        /// <param name="log"><see cref="ILogger"/> instance.</param>
        /// <returns>Open API document in a format of either JSON or YAML.</returns>
        [FunctionName(nameof(RenderOpenApiDocument))]
        [OpenApiIgnore]
        public async Task<IActionResult> RenderOpenApiDocument(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "openapi/{version}.{extension}")] HttpRequest req,
            string version,
            string extension,
            ILogger log)
        {
            var options = new RenderOpenApiDocumentFunctionOptions(version, extension, Assembly.GetExecutingAssembly());
            var result = await this._openApiDoc
                                   .AddLogger(log)
                                   .InvokeAsync<HttpRequest, IActionResult>(req, options)
                                   .ConfigureAwait(false);

            return result;
        }

        /// <summary>
        /// Invokes the HTTP trigger endpoint to render Swagger UI in HTML.
        /// </summary>
        /// <param name="req"><see cref="HttpRequest"/> instance.</param>
        /// <param name="log"><see cref="ILogger"/> instance.</param>
        /// <returns>Swagger UI in HTML.</returns>
        [FunctionName(nameof(RenderSwaggerUI))]
        [OpenApiIgnore]
        public async Task<IActionResult> RenderSwaggerUI(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "swagger/ui")] HttpRequest req,
            ILogger log)
        {
            var options = new RenderSwaggerUIFunctionOptions();
            var result = await this._swaggerUi
                                   .InvokeAsync<HttpRequest, IActionResult>(req, options)
                                   .ConfigureAwait(false);

            return result;
        }
    }
}
