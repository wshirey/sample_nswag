using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Humanizer;
using NSwag.SwaggerGeneration.Processors;
using NSwag.SwaggerGeneration.Processors.Contexts;

namespace sample_nswag.NSwag
{
  public class CustomOperationProcessor : IOperationProcessor
  {
    public Task<bool> ProcessAsync(OperationProcessorContext context)
    {
        var operation = context.OperationDescription.Operation;
        var descriptionAttribute = context.MethodInfo.GetCustomAttribute(typeof(DescriptionAttribute));

        if (descriptionAttribute != null)
            operation.Description = (descriptionAttribute as DescriptionAttribute).Description;

        operation.OperationId =
            operation.OperationId.Split("_").Last();

        operation.Summary =
            operation.OperationId.Humanize(LetterCasing.Title);

        return Task.FromResult(true);
    }
  }
}
