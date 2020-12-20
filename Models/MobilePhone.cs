using System;

namespace DotnetCoreRESTAPI.Models
{
    public record MobilePhone
    {
        public Guid Id { get; init; }
        public string Type { get; init; }
        public string Name { get; init; }
        public decimal OriginPrice { get; init; }
        public double Discount { get; init; }
        public DateTimeOffset CreatedDate { get; init; }
        public DateTimeOffset? ModifiedDate { get; init; }
    }
}