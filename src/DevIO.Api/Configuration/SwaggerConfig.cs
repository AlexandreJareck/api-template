namespace DevIO.Api.Configuration;

public static class SwaggerConfig
{
    public static IApplicationBuilder UseSwaggerConfig(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();

        return app;
    }
}
