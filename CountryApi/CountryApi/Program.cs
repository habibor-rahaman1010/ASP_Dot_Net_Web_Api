using CountryApi.DataAccessLayer;
using CountryApi.MapsterProfile;
using Mapster;
using Microsoft.EntityFrameworkCore;


    WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

try 
{

    // Add services to the container.

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    // Database connectiong here...
    builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

    //Mapster Registration here...
    TypeAdapterConfig.GlobalSettings.Apply(new MappingProfile());

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();


    app.Run();
}
catch (Exception ex) 
{
    Console.WriteLine(ex.ToString());
}
finally 
{
    Console.WriteLine("Application Crush!");
}
