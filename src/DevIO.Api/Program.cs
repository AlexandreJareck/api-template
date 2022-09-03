using DevIO.Api.Configuration;

var builder = WebApplication.CreateBuilder(args);

ServicesConfig.UseServices(builder);

AppConfig.UseApp(builder);

