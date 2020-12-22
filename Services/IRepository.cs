using System;
using System.Collections.Generic;
using DotnetCoreRESTAPI.Models;

namespace DotnetCoreRESTAPI.Services
{
    public interface IRepository
    {
        MobilePhone GetMobilePhone(Guid id);
        IEnumerable<MobilePhone> GetMobilePhones();
        void CreateMobilePhone(MobilePhone mobilePhone);
        void UpdateMobilePhone(MobilePhone mobilePhone);
        void DeleteMobilePhone(Guid id);
    }
}