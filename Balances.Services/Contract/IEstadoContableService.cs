using Balances.DTO;
using Balances.Model;

namespace Balances.Services.Contract
{
    public interface IEstadoContableService
    {
        EstadoContable ActualizarEstadoContable(ActualizarEstadoContableDto estadoContable);

        public EstadoContable Get(string balanceId);
    }
}
