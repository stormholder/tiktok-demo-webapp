namespace TikTok.Web.StartupExtensions;

public static class AuthExtension
{
    public static IServiceCollection AddTikTokAuth(this IServiceCollection services)
    {
        services.AddAuthentication(options => {
            options.DefaultAuthenticateScheme = "TikTok";
        })
        .AddOAuth("TikTok", options => {
            options.ClientId = "";
            options.ClientSecret = "";
            options.SaveTokens = true;
            options.Scope.Add("user.info.basic");
            options.Scope.Add("video.list");
        });
        return services;
    }

    public static IApplicationBuilder UseTikTokAuth(this IApplicationBuilder app)
    {
        return app;
    }
}
