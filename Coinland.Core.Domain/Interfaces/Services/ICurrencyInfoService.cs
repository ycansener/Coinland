using Coinland.Core.Domain.Entities;
using System.Collections.Generic;

namespace Coinland.Core.Domain.Interfaces.Services
{
    public interface ICurrencyInfoService
    {
        List<CurrencyInfoModel> GetCurrencyInfo(int? start, int? limit, string convert);
    }
}
