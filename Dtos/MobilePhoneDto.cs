using System;

namespace DotnetCoreRESTAPI.Dtos
{
    public record MobilePhoneDto
    {
        public Guid Id { get; init; }
        public string Type { get; init; }
        public string Name { get; init; }
        public decimal SellPrice { get; init; }
        public DateTimeOffset CreatedDate { get; init; }
        public DateTimeOffset? ModifiedDate { get; init; }
    }
}