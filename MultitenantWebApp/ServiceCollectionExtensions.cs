using System;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace MultitenantWebApp
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMultitenancy(this IServiceCollection services)
        {
            services.AddScoped<TenantContext>();

            services.AddScoped<ITenantContext>(provider =>
                provider.GetRequiredService<TenantContext>());

            services.AddScoped<ITenantSetter>(provider =>
                provider.GetRequiredService<TenantContext>());

            services.AddScoped<ITenantStore, TenantStore>();
            return services;
        }

        public static IServiceCollection AddMultitenantAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie(options =>
            {
                //options.Cookie.Path = "/";
            });

            services.AddPerRequestCookieOptions((options, httpContext) =>
            {
                var tenant = httpContext
                    .RequestServices.GetRequiredService<ITenantContext>()
                    .CurrentTenant;

                options.DataProtectionProvider = httpContext
                    .RequestServices.GetRequiredService<IDataProtectionProvider>()
                    .CreateProtector(tenant.Name);
            });

            return services;
        }

        private static void AddPerRequestCookieOptions(
            this IServiceCollection services,
            Action<CookieAuthenticationOptions, HttpContext> configAction)
        {
            services.AddScoped<IOptionsMonitor<CookieAuthenticationOptions>, CookieOptionsProvider>();
            services.AddScoped<IConfigureOptions<CookieAuthenticationOptions>, CookieOptionsInitializer>();
            services.AddSingleton(new CookieOptionsConfigurator(configAction));
        }
    }
}