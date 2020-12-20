using System.ComponentModel.DataAnnotations;

namespace DotnetCoreRESTAPI.Dtos
{
    public record MobilePhoneREST
    {
        [Required]
        [MinLength(6)]
        [MaxLength(20)]
        public string Type { get; init; }
        [Required]
        [MinLength(6)]
        [MaxLength(20)]
        public string Name { get; init; }
        [Required]
        [Range(0, 20000)]
        public decimal OriginPrice { get; init; }
        [Required]
        [Range(0.5, 1.0)]
        public double Discount { get; init; }
    }
}