using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DotnetCoreRESTAPI.Models;

namespace DotnetCoreRESTAPI.Services
{
    public interface IRepository
    {
        Task<MobilePhone> GetMobilePhoneAsync(Guid id);
        Task<IEnumerable<MobilePhone>> GetMobilePhonesAsync();
        Task CreateMobilePhoneAsync(MobilePhone mobilePhone);
        Task UpdateMobilePhoneAsync(MobilePhone mobilePhone);
        Task DeleteMobilePhoneAsync(Guid id);
    }
}