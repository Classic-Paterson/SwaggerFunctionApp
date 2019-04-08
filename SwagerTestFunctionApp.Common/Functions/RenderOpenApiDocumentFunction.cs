using Aliencube.AzureFunctions.Extensions.DependencyInjection.Abstractions;
using Aliencube.AzureFunctions.Extensions.OpenApi.Abstractions;
using Aliencube.AzureFunctions.Extensions.OpenApi.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SwagerTestFunctionApp.Common.Configurations;
using SwagerTestFunctionApp.Common.Functions.FunctionOptions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SwagerTestFunctionApp.Common.Functions
{
    /// <summary>
    /// This represents the function entity to render Open API document.
    /// </summary>
    public class RenderOpenApiDocumentFunction : FunctionBase<ILogger>, IRenderOpenApiDocumentFunction
    {
        private readonly AppSettings _settings;
        private readonly IDocument _document;

        /// <summary>
        /// Initializes a new instance of the <see cref="RenderOpeApiDocumentFunction"/> class.
        /// </summary>
        /// <param name="settings"><see cref="AppSettings"/> instance.</param>
        /// <param name="document"><see cref="IDocument"/> instance.</param>
        public RenderOpenApiDocumentFunction(AppSettings settings, IDocument document)
        {
            this._settings = settings;
            this._document = document;
        }

        /// <inheritdoc />
        public override async Task<TOutput> InvokeAsync<TInput, TOutput>(TInput input, FunctionOptionsBase options = null)
        {
            var req = input as HttpRequest;
            var opt = options as RenderOpenApiDocumentFunctionOptions;

            var contentType = opt.Format.GetContentType();
            var result = await this._document
                                   .InitialiseDocument()
                                   .AddMetadata(this._settings.OpenApiInfo)
                                   .AddServer(req, this._settings.HttpSettings.RoutePrefix)
                                   .Build(opt.Assembly)
                                   .RenderAsync(opt.Version, opt.Format)
                                   .ConfigureAwait(false);

            var content = new ContentResult()
                              {
                                  Content = result,
                                  ContentType = contentType,
                                  StatusCode = (int)HttpStatusCode.OK
                              };

            return (TOutput)(IActionResult)content;

        }
    }
}