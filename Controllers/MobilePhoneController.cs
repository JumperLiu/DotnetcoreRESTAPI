using System;
using System.Collections.Generic;
using System.Linq;
using DotnetCoreRESTAPI.Dtos;
using DotnetCoreRESTAPI.Extsions;
using DotnetCoreRESTAPI.Models;
using DotnetCoreRESTAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace DotnetCoreRESTAPI.Controllers
{
    [ApiController]
    [Route("MobilePhone")]
    public class MobilePhoneController : ControllerBase
    {
        private readonly IRepository _repository;

        public MobilePhoneController(IRepository repository) => _repository = repository;
        // GET /MobilePhone/
        [HttpGet]
        public IEnumerable<MobilePhoneDto> GetList() => _repository.GetMobilePhones().Select(i => i.AsDto());
        // GET /MobilePhone/{id}
        [HttpGet("{id}")]
        public MobilePhoneDto GetOne(Guid id) => _repository.GetMobilePhone(id).AsDto();
        // POST /MobilePhone/
        [HttpPost]
        public ActionResult<MobilePhoneDto> CreateOne(MobilePhoneREST mobilePhone)
        {
            var mobile = new MobilePhone()
            {
                Id = Guid.NewGuid(),
                Type = mobilePhone.Type,
                Name = mobilePhone.Name,
                OriginPrice = mobilePhone.OriginPrice,
                Discount = mobilePhone.Discount,
                CreatedDate = DateTimeOffset.Now
            };
            _repository.CreateMobilePhone(mobile);
            return CreatedAtAction(nameof(GetOne), new { id = mobile.Id }, mobile.AsDto());
        }
        // PUT /MobilePhone/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateOne(Guid id, MobilePhoneREST mobilePhone)
        {
            var existMobilePhone = _repository.GetMobilePhone(id);
            if (existMobilePhone == null) return NotFound($"您所要更新的 ID 为 {id} 不存在，更新失败。");
            var update = existMobilePhone with
            {
                Type = mobilePhone.Type,
                Name = mobilePhone.Name,
                OriginPrice = mobilePhone.OriginPrice,
                Discount = mobilePhone.Discount,
                ModifiedDate = DateTimeOffset.Now
            };
            _repository.UpdateMobilePhone(update);
            return Ok("更新成功。");
        }
        // DELETE /MobilePhone/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteOne(Guid id)
        {
            var existMobilePhone = _repository.GetMobilePhone(id);
            if (existMobilePhone == null) return NotFound($"您所要删除的 ID 为 {id} 不存在，无需删除操作。");
            _repository.DeleteMobilePhone(id);
            return Ok("删除成功。");
        }
    }
}