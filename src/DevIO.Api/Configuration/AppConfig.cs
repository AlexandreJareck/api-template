namespace DevIO.Api.Configuration;

public class AppConfig
{
    public static void UseApp(WebApplicationBuilder builder)
    {
        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            SwaggerConfig.UseSwagger(app);
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
