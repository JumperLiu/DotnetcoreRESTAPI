using DotnetCoreRESTAPI.Dtos;
using DotnetCoreRESTAPI.Models;

namespace DotnetCoreRESTAPI.Extsions
{
    public static class Common
    {
        public static MobilePhoneDto AsDto(this MobilePhone mobilePhone) => new MobilePhoneDto()
        {
            Id = mobilePhone.Id,
            Type = mobilePhone.Type,
            Name = mobilePhone.Name,
            SellPrice = mobilePhone.OriginPrice * (decimal)mobilePhone.Discount,
            CreatedDate = mobilePhone.CreatedDate,
            ModifiedDate = mobilePhone.ModifiedDate
        };
    }
}