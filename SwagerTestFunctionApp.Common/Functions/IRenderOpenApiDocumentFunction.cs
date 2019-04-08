using Aliencube.AzureFunctions.Extensions.DependencyInjection.Abstractions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace SwagerTestFunctionApp.Common.Functions
{
    public interface IRenderOpenApiDocumentFunction : IFunction<ILogger>
    {
    }
}