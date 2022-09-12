
namespace TikTok.Web.StartupExtensions;

public static class AuthExtension
{
    public static IServiceCollection AddTikTokAuth(
        this IServiceCollection services, 
        IConfiguration configuration
    )
    {
        services.AddAuthentication(options => {
            options.DefaultAuthenticateScheme = "TikTok";
        })
        .AddTikTok(tiktokOptions => {
            tiktokOptions.ClientId = configuration["Authentication:TikTok:ClientId"];
            tiktokOptions.ClientSecret = configuration["Authentication:TikTok:ClientSecret"];
        });
        return services;
    }

    public static IApplicationBuilder UseTikTokAuth(this IApplicationBuilder app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
        return app;
    }
}
