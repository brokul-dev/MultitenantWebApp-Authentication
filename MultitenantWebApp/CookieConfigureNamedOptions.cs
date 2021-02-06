using System;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace MultitenantWebApp
{
    public class CookieConfigureNamedOptions 
        : IConfigureNamedOptions<CookieAuthenticationOptions>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly Action<CookieAuthenticationOptions, HttpContext> _configureAction;

        public CookieConfigureNamedOptions(
            IHttpContextAccessor httpContextAccessor,
            Action<CookieAuthenticationOptions, HttpContext> configureAction)
        {
            _httpContextAccessor = httpContextAccessor;
            _configureAction = configureAction;
        }

        public void Configure(string name, CookieAuthenticationOptions options)
        {
            if (!string.Equals(name, 
                CookieAuthenticationDefaults.AuthenticationScheme,
                StringComparison.Ordinal))
            {
                return;
            }

            if (_httpContextAccessor?.HttpContext == null)
            {
                throw new ArgumentNullException(nameof(IHttpContextAccessor.HttpContext));
            }

            _configureAction(options, _httpContextAccessor.HttpContext);
        }

        public void Configure(CookieAuthenticationOptions options) 
            => throw new NotImplementedException();
    }
}