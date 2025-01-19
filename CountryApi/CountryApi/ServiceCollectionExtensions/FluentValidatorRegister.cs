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
            //Autometic process
            //services.AddValidatorsFromAssemblyContaining<CountryValidator>();
            //services.AddValidatorsFromAssemblyContaining<CountryUpdateValidator>();

            //Menual Process
            services.AddScoped<IValidator<CountryDto>, CountryValidator>();
            services.AddScoped<IValidator<UpdateDto>, CountryUpdateValidator>();
        }
    }
}
