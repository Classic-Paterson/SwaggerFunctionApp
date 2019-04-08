using Aliencube.AzureFunctions.Extensions.DependencyInjection.Abstractions;
using Microsoft.Extensions.Logging;

namespace SwagerTestFunctionApp.Common.Functions
{
    public interface IRenderSwaggerUIFunction : IFunction<ILogger>
    {
    }
}
