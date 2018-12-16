using System.Globalization;
using FluentValidation;
using FluentValidation.Resources;
using Microsoft.Extensions.Localization;

namespace Desafio.Business
{
    internal class ValidationLanguageManager : ILanguageManager
    {
        private readonly IStringLocalizer stringLocalizer;

        public ValidationLanguageManager(IStringLocalizer stringLocalizer)
        {         
            this.stringLocalizer = stringLocalizer;
            ValidatorOptions.DisplayNameResolver = (t, m, l) => stringLocalizer[m.Name].Value;
        }

        public bool Enabled { get; set; }
        public CultureInfo Culture { get; set; }

        public string GetString(string key, CultureInfo culture = null)
        {
            return stringLocalizer[key].Value;
        }
    }
}
