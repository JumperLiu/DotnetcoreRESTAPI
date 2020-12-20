using System;
using System.Collections.Generic;
using System.Linq;
using DotnetCoreRESTAPI.Models;
using DotnetCoreRESTAPI.Services;

namespace DotnetCoreRESTAPI.Repositories
{
    public class InMemoryRepository : IRepository
    {
        private readonly List<MobilePhone> _repository = new();

        public InMemoryRepository()
        {
            for (int i = 1, loopMax = 10; i <= loopMax; i++)
            {
                _repository.Add(new MobilePhone()
                {
                    Id = Guid.NewGuid(),
                    Type = $"Apple_Proc_{i}",
                    Name = $"Apple_{new Random().Next(4, 8)}s_Plus_{i}",
                    OriginPrice = new Random().Next(5000, 10000) / (decimal)1.0,
                    Discount = new Random().Next(88, 95) / (double)100,
                    CreatedDate = DateTimeOffset.Now
                });
            }
        }

        public List<MobilePhone> GetMobilePhones() => _repository;

        public MobilePhone GetMobilePhone(Guid id) => _repository.Where(i => i.Id == id).SingleOrDefault();

        public void CreateMobilePhone(MobilePhone mobilePhone) => _repository.Add(mobilePhone);

        public void UpdateMobilePhone(MobilePhone mobilePhone)
        => _repository[_repository.FindIndex(i => i.Id == mobilePhone.Id)] = mobilePhone;

        public void DeleteMobilePhone(Guid id)
        => _repository.RemoveAt(_repository.FindIndex(i => i.Id == id));
    }
}