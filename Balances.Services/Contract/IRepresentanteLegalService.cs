using Balances.DTO;
using Balances.Model;

namespace Balances.Services.Contract
{
    public interface IRepresentanteLegalService
    {
        void PostRepresentante(AltaRepresentanteLegalDto representanteLegal);

        List<RepresentanteLegal> GetAll(string balanceId);

        void Delete(string balanceId, string id);
    }
}
