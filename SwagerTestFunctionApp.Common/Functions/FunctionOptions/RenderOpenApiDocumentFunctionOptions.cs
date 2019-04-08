using Aliencube.AzureFunctions.Extensions.DependencyInjection.Abstractions;
using Microsoft.OpenApi;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace SwagerTestFunctionApp.Common.Functions.FunctionOptions
{
    public class RenderOpenApiDocumentFunctionOptions : FunctionOptionsBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RenderOpeApiDocumentFunctionOptions"/> class.
        /// </summary>
        /// <param name="version">Open API version. This MUST be either "v2" or "v3".</param>
        /// <param name="format">Open API document format. This MUST be either "json" or "yaml".</param>
        /// <param name="assembly">Function app assembly.</param>
        public RenderOpenApiDocumentFunctionOptions(string version, string format, Assembly assembly)
        {
            this.Version = this.GetVersion(version);
            this.Format = this.GetFormat(format);
            this.Assembly = assembly ?? throw new ArgumentNullException(nameof(assembly));
        }

        /// <summary>
        /// Gets or sets the <see cref="OpenApiSpecVersion"/> value.
        /// </summary>
        public OpenApiSpecVersion Version { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="OpenApiFormat"/> value.
        /// </summary>
        public OpenApiFormat Format { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="System.Reflection.Assembly"/> instance.
        /// </summary>
        public Assembly Assembly { get; set; }

        private OpenApiSpecVersion GetVersion(string version)
        {
            if (string.IsNullOrWhiteSpace(version))
            {
                throw new ArgumentNullException(nameof(version));
            }

            if (version.Equals("v2", StringComparison.CurrentCultureIgnoreCase))
            {
                return OpenApiSpecVersion.OpenApi2_0;
            }

            if (version.Equals("v3", StringComparison.CurrentCultureIgnoreCase))
            {
                return OpenApiSpecVersion.OpenApi3_0;
            }

            throw new InvalidOperationException("Invalid Open API version");
        }

        private OpenApiFormat GetFormat(string format)
        {
            if (string.IsNullOrWhiteSpace(format))
            {
                throw new ArgumentNullException(nameof(format));
            }

            return Enum.TryParse<OpenApiFormat>(format, true, out OpenApiFormat result)
                       ? result
                       : throw new InvalidOperationException("Invalid Open API format");
        }
    }
}