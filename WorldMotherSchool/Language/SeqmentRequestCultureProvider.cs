using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorldMotherSchool.Language
{
    public class SeqmentRequestCultureProvider : IRequestCultureProvider
    {
        public Task<ProviderCultureResult> DetermineProviderCultureResult(HttpContext httpContext)
        {
            ProviderCultureResult cultureResult = null;
            if(httpContext.Request.Path.HasValue)
            {
                string[] segments = httpContext.Request.Path.Value.Split("/");

                string givenLang = segments[1];
                if(SupportedLanguage.IsLanguageSupported(givenLang))
                {
                    cultureResult = new ProviderCultureResult(SupportedLanguage.GetLanguage(givenLang));
                }
                else
                {
                    cultureResult = new ProviderCultureResult(SupportedLanguage.DefaultLanguage);
                }
            }
            else
            {
                cultureResult = new ProviderCultureResult(SupportedLanguage.DefaultLanguage);
            }
            return Task.FromResult(cultureResult);
        }
    }
}
