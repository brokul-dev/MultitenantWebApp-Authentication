using System;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace MultitenantWebApp
{
    public class CookieConfigureNamedOptions : IConfigureNamedOptions<CookieAuthenticationOptions>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly CookieOptionsConfigurator _optionsConfigurator;

        public CookieConfigureNamedOptions(
            IHttpContextAccessor httpContextAccessor, 
            CookieOptionsConfigurator optionsConfigurator)
        {
            _httpContextAccessor = httpContextAccessor;
            _optionsConfigurator = optionsConfigurator;
        }

        public void Configure(string name, CookieAuthenticationOptions options)
        {
            if (!string.Equals(name, CookieAuthenticationDefaults.AuthenticationScheme, StringComparison.Ordinal))
            {
                return;
            }

            if (_httpContextAccessor?.HttpContext == null)
            {
                throw new ArgumentNullException(nameof(IHttpContextAccessor.HttpContext));
            }

            _optionsConfigurator.Run(options, _httpContextAccessor.HttpContext);
        }

        public void Configure(CookieAuthenticationOptions options) 
            => throw new NotImplementedException();
    }
}