using CountryApi.Model;
using Microsoft.EntityFrameworkCore;

namespace CountryApi.DataAccessLayer
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) 
        { 
        
        } 
        public DbSet<Country> Countries { get; set; }
    }
}
