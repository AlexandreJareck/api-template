﻿using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace DevIO.Api.Configuration;

public static class AppConfig
{
    public static IServiceCollection AddApiConfig(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();

        services.AddApiVersioning(options =>
        {
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.ReportApiVersions = true;
        });

        services.AddVersionedApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });

        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });

        services.AddCors(options =>
        {
            options.AddPolicy("Development", builder =>
                builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());

            options.AddDefaultPolicy(builder =>
                builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());

            options.AddPolicy("Production", builder =>
                builder
                    .WithMethods("GET")
                    .WithOrigins("http://meusite.io")
                    .SetIsOriginAllowedToAllowWildcardSubdomains()
                    //.WithHeaders(HeaderNames.ContentType, "x-custom-header")
                    .AllowAnyHeader());
        });

        return services;
    }

    public static IApplicationBuilder UseApiConfig(this IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseCors("Development");
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseCors("Production");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapHealthChecks("/api/hc", new HealthCheckOptions()
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });
            endpoints.MapHealthChecksUI(options =>
            {
                options.UIPath = "/api/hc-ui";
                options.ResourcesPath = "/api/hc-ui-resources";

                options.UseRelativeApiPath = false;
                options.UseRelativeResourcesPath = false;
                options.UseRelativeWebhookPath = false;
            });

        });

        return app;
    }
}
