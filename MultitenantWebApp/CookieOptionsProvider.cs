using System;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;

namespace MultitenantWebApp
{
    public class CookieOptionsProvider : IOptionsMonitor<CookieAuthenticationOptions>
    {
        private readonly IOptionsFactory<CookieAuthenticationOptions> _optionsFactory;

        public CookieOptionsProvider(IOptionsFactory<CookieAuthenticationOptions> optionsFactory)
        {
            _optionsFactory = optionsFactory;
        }

        public CookieAuthenticationOptions CurrentValue => Get(Options.DefaultName);

        public CookieAuthenticationOptions Get(string name)
        {
            return _optionsFactory.Create(name);
        }

        public IDisposable OnChange(Action<CookieAuthenticationOptions, string> listener) => null;
    }
}