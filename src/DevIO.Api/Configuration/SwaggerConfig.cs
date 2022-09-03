namespace DevIO.Api.Configuration;

public class SwaggerConfig
{
    public static void UseSwagger(WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
}
