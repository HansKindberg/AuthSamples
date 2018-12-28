using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace AuthSamples.DynamicSchemes.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthenticationSchemeProvider _schemeProvider;
        private readonly IOptionsMonitorCache<AuthenticationSchemeOptions> _optionsCache;

        public AuthController(IAuthenticationSchemeProvider schemeProvider, IOptionsMonitorCache<AuthenticationSchemeOptions> optionsCache)
        {
            _schemeProvider = schemeProvider;
            _optionsCache = optionsCache;
        }

        public IActionResult Remove(string scheme)
        {
            _schemeProvider.RemoveScheme(scheme);
            _optionsCache.TryRemove(scheme);
            return Redirect("/");
        }


	    public virtual IActionResult Yxi()
	    {
			var a = JsonConvert.DeserializeObject<GoogleOptions>("{\"ClientId\":\"Client-id\",\"Client-secret\":null,\"SignInScheme\":\"Google\"}");

			/*
			
			 */
			return Redirect("/");
	    }

		[HttpPost]
        public async Task<IActionResult> AddOrUpdate(string scheme, string optionsMessage)
        {
            if (await _schemeProvider.GetSchemeAsync(scheme) == null)
            {
                _schemeProvider.AddScheme(new AuthenticationScheme(scheme, scheme, typeof(GoogleHandler)));
            }
            else
            {
                _optionsCache.TryRemove(scheme);
            }
            _optionsCache.TryAdd(scheme, new GoogleOptions
            {
				SignInScheme = "Kalle",
				ClientId = "708996912208-9m4dkjb5hscn7cjrn5u0r4tbgkbj1fko.apps.googleusercontent.com",
	            ClientSecret = "wdfPY6t8H8cecgjlxud__4Gh"
			//DisplayMessage = optionsMessage
		});

			//var a = await this._schemeProvider.GetSchemeAsync("Google");

	        //return this.Challenge(new AuthenticationProperties
	        //{

	        //}, "Google");

            return Redirect("/");
        }
    }
}
