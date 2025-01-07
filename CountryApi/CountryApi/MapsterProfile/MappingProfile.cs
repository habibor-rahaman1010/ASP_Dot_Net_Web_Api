using CountryApi.Model;
using Mapster;

namespace CountryApi.MapsterProfile
{
    public class MappingProfile : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<CountryDto, Country>();

            config.NewConfig<UpdateDto, Country>();


           /* // Define the mapping between CountryDto and Country
            config.NewConfig<CountryDto, Country>()
                  .Map(dest => dest.Id, src => Guid.NewGuid())
                  .Map(dest => dest.Name, src => src.Name)
                  .Map(dest => dest.Capital, src => src.Capital)
                  .Map(dest => dest.Population, src => src.Population)
                  .Map(dest => dest.Area, src => src.Area)
                  .Map(dest => dest.Currency, src => src.Currency);

            config.NewConfig<Country, UpdateDto>()
                  .Map(dest => dest.Name, src => src.Name)
                  .Map(dest => dest.Capital, src => src.Capital)
                  .Map(dest => dest.Population, src => src.Population)
                  .Map(dest => dest.Area, src => src.Area)
                  .Map(dest => dest.Currency, src => src.Currency);*/
        }
    }
}






/*
public static class MapsterExtensions
{
    public static void AddMapsterConfigurations(this IServiceCollection services)
    {
        TypeAdapterConfig<SourceClass, DestinationClass>
            .NewConfig()
            .Map(dest => dest.Property1, src => src.AnotherProperty);

        // Apply global settings
        TypeAdapterConfig.GlobalSettings.Scan(typeof(SourceClass).Assembly);
    }
}
*/
