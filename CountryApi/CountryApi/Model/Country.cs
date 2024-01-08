using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace CountryApi.Model
{
    public class Country
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Capital { get; set; }
        public int? Population { get; set; }
        public double? Area { get; set; }
        public string? Currency { get; set; }
    }
}
