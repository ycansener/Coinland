using Coinland.Core.Domain.Entities;
using Coinland.Core.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Coinland.Core.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyInfoController : ControllerBase
    {
        public ICurrencyInfoService _service;
        public CurrencyInfoController(ICurrencyInfoService service)
        {
            _service = service;
        }
        [HttpGet]
        public ActionResult<IEnumerable<CurrencyInfoModel>> Get(string convert, int? start, int? limit)
        {
            var result = _service.GetCurrencyInfo(start, limit, convert);
            return result;
        }
    }
}