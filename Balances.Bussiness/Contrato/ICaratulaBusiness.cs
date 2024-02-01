using Balances.DTO;
using Balances.Model;

namespace Balances.Bussiness.Contrato
{
    public interface ICaratulaBusiness
    {

        ResponseDTO<Balance> Insert(CaratulaDto modelo);

        bool Update(CaratulaDto modelo);

        bool Delete(CaratulaDto modelo);

        //ResponseDTO<CaratulaDto> GetById(string id);


        IEnumerable<CaratulaDto> List();
    }
}
