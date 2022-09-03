using DevIO.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DevIO.Api.Configuration;

public class ServicesConfig
{
    public static void UseServices(WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddDbContext<MyDbContext>(x => x.UseSqlServer(builder.Configuration
            .GetConnectionString("DefaultConnection")));

        builder.Services.AddAutoMapper(typeof(Program));
        builder.Services.SetDependencies();
    }
}
