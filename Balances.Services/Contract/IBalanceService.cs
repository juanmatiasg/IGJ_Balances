using Balances.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balances.Services.Contract
{
    public interface IBalanceService
    {
        BalanceResponseDTO Create(BalanceRequestDTO modelo);
        bool Delete(string id);
        BalanceResponseDTO GetById(string id);
        List<BalanceResponseDTO> GetAll();
        bool Update(BalanceRequestDTO modelo);

    }
}
