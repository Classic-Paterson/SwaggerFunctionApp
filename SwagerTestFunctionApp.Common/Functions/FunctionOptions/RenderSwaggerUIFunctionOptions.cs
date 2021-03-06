﻿using Aliencube.AzureFunctions.Extensions.DependencyInjection.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace SwagerTestFunctionApp.Common.Functions.FunctionOptions
{
   public class RenderSwaggerUIFunctionOptions : FunctionOptionsBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RenderOpeApiDocumentFunctionOptions"/> class.
        /// </summary>
        /// <param name="endpoint">Function app endpoint for Swagger document.</param>
        public RenderSwaggerUIFunctionOptions(string endpoint = "swagger.json")
        {
            this.Endpoint = endpoint ?? throw new ArgumentNullException(nameof(endpoint));
        }

        /// <summary>
        /// Gets or sets the endpoint of the Swagger document.
        /// </summary>
        public string Endpoint { get; set; }
    }
}
