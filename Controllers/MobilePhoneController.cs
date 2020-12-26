using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task<IEnumerable<MobilePhoneDto>> GetListAsync() => (await _repository.GetMobilePhonesAsync()).Select(i => i.AsDto());
        // GET /MobilePhone/{id}
        [HttpGet("{id}")]
        public async Task<MobilePhoneDto> GetOneAsync(Guid id) => (await _repository.GetMobilePhoneAsync(id)).AsDto();
        // POST /MobilePhone/
        [HttpPost]
        public async Task<ActionResult<MobilePhoneDto>> CreateOneAsync(MobilePhoneREST mobilePhone)
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
            await _repository.CreateMobilePhoneAsync(mobile);
            return CreatedAtAction(nameof(GetOneAsync), new { id = mobile.Id }, mobile.AsDto());
        }
        // PUT /MobilePhone/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateOneAsync(Guid id, MobilePhoneREST mobilePhone)
        {
            var existMobilePhone = await _repository.GetMobilePhoneAsync(id);
            if (existMobilePhone == null) return NotFound($"您所要更新的 ID 为 {id} 不存在，更新失败。");
            var update = existMobilePhone with
            {
                Type = mobilePhone.Type,
                Name = mobilePhone.Name,
                OriginPrice = mobilePhone.OriginPrice,
                Discount = mobilePhone.Discount,
                ModifiedDate = DateTimeOffset.Now
            };
            await _repository.UpdateMobilePhoneAsync(update);
            return Ok("更新成功。");
        }
        // DELETE /MobilePhone/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOneAsync(Guid id)
        {
            var existMobilePhone = await _repository.GetMobilePhoneAsync(id);
            if (existMobilePhone == null) return NotFound($"您所要删除的 ID 为 {id} 不存在，无需删除操作。");
            await _repository.DeleteMobilePhoneAsync(id);
            return Ok("删除成功。");
        }
    }
}