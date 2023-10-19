using Asp.Versioning.ApiExplorer;

using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.SwaggerGen;

using System.Text;

namespace Web.Api.Swagger;

public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
{
    private readonly IApiVersionDescriptionProvider provider;
    private readonly IHostEnvironment environment;

    public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider,
        IHostEnvironment environment)
    {
        this.provider = provider;
        this.environment = environment;
    }

    public void Configure(SwaggerGenOptions options)
    {
        foreach (var description in provider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
        }
    }

    private OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
    {
        var text = new StringBuilder("API responsible for complete management of the Ctrl+Eat Fast Food restaurant");
        var info = new OpenApiInfo()
        {
            Title = environment.ApplicationName,
            Version = description.ApiVersion.ToString(),
            Contact = new OpenApiContact() { Name = "Jose Felipe Blum de Araujo", Email = "contato@jsfelipearaujo.com" },
        };

        if (description.IsDeprecated)
        {
            text.Append(" This version of the API has been deprecated.");
        }

        info.Description = text.ToString();

        return info;
    }
}
