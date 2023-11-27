using Balances.DTO;

namespace Balances.Services.Contract
{
    public interface IPresentacionService
    {

        string PlantillaPresentacionHtml(BalanceDtoPresentacion balance, string qr);

        BalanceDtoPresentacion GetBalanceAutoridadySocioFirmante(BalanceDto balance);

        string CrearMailPresentacion(BalanceDtoPresentacion balance, string qr);
    }
}
