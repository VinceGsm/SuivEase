using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

public class EnumSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.Type.IsEnum)
        {
            schema.Enum.Clear();
            var enumNames = Enum.GetNames(context.Type).Select(n => new OpenApiString(n));
            foreach (var name in enumNames)
            {
                schema.Enum.Add(name);
            }
        }
    }
}