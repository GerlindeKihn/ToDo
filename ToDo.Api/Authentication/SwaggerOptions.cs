using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ToDo.Api.Authentication;

public static class SwaggerOptions
{
    public static void SetupBasicAuthorization(SwaggerGenOptions options)
    {
        const string basic = "basic";

        options.AddSecurityDefinition(basic, new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            Scheme = basic,
            In = ParameterLocation.Header,
            Description = "Basic Authorization Header using"
        });
        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = basic
                    }
                },
                new string[] {}
            }
        });
    }
}