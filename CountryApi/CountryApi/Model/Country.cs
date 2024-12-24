using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace CountryApi.Model
{
    public class Country
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Capital { get; set; } = string.Empty;
        public int Population { get; set; }
        public double Area { get; set; }
        public string Currency { get; set; } = string.Empty;
        public bool IsFreedom { get; set; }
    }
}
