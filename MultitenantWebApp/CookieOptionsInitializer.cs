using System;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace MultitenantWebApp
{
    public class CookieOptionsInitializer : IConfigureNamedOptions<CookieAuthenticationOptions>
    {
        private readonly CookieOptionsConfigurator _optionsConfigurator;
        private readonly HttpContext _httpContext;

        public CookieOptionsInitializer(
            IHttpContextAccessor httpContextAccessor, 
            CookieOptionsConfigurator optionsConfigurator)
        {
            _optionsConfigurator = optionsConfigurator;
            _httpContext = httpContextAccessor.HttpContext;
        }

        public void Configure(string name, CookieAuthenticationOptions options)
        {
            if (!string.Equals(name, CookieAuthenticationDefaults.AuthenticationScheme, StringComparison.Ordinal))
            {
                return;
            }

            _optionsConfigurator.Run(options, _httpContext);
        }

        public void Configure(CookieAuthenticationOptions options) 
            => throw new NotImplementedException();
    }
}