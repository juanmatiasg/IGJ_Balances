using Balances.DTO;

namespace Balances.Services.Contract
{
    public interface IContadorService
    {
        ContadorDto ActualizarContador(ActualizarContadorDto contador);

        ContadorDto Get(string balanceId);
    }
}
