using CountryApi.Model;
using CountryApi.ModelValidators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using System.Runtime.CompilerServices;

namespace CountryApi.ServiceCollectionExtensions
{
    public static class FluentValidatorRegister
    {
        public static void AddFluentValidatorRegister (this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<CountryValidator>();
            services.AddValidatorsFromAssemblyContaining<CountryUpdateValidator>();         
        }
    }
}
