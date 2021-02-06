using System;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace MultitenantWebApp
{
    public class CookieOptionsConfigurator
    {
        private readonly Action<CookieAuthenticationOptions, HttpContext> _configure;

        public CookieOptionsConfigurator(Action<CookieAuthenticationOptions, HttpContext> configure)
        {
            _configure = configure;
        }

        public void Run(CookieAuthenticationOptions options, HttpContext httpContext)
        {
            _configure(options, httpContext);
        }
    }
}