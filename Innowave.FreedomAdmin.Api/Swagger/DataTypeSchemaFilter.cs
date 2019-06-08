using System.ComponentModel.DataAnnotations;
using FluentMigrator.Infrastructure.Extensions;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Innowave.FreedomAdmin.Api.Swagger
{
    public class DataTypeSchemaFilter : ISchemaFilter
    {
        public void Apply(Schema schema, SchemaFilterContext context)
        {
            if (schema.Properties == null) return;

            foreach (var key in schema.Properties.Keys)
            {
                var dataType = context.SystemType.GetProperty(key.ToPascalCase())?.GetOneAttribute<DataTypeAttribute>();
                if (dataType != null)
                {
                    switch (dataType.DataType)
                    {
                        case DataType.Date:
                            schema.Properties[key].Format = "date";
                            break;
                        case DataType.Password:
                            schema.Properties[key].Format = "password";
                            break;
                        case DataType.EmailAddress:
                            schema.Properties[key].Example = "support@innowave.in";
                            break;
                        case DataType.PhoneNumber:
                            schema.Properties[key].Example = "+91 12345 67890";
                            break;
                        case DataType.Time:
                            schema.Properties[key].Pattern = "[0-2]?[0-9]:[0-9][0-9]";
                            schema.Properties[key].Example = "12:00";
                            break;
                    }
                }
            }
        }
    }
}
