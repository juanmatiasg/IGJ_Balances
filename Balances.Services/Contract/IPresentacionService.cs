using Balances.DTO;

namespace Balances.Services.Contract
{
    public interface IPresentacionService
    {

        string CrearPlantillaPresentacionPdf(BalanceDtoPresentacion balance, string qr);

        BalanceDtoPresentacion GetBalanceAutoridadySocioFirmante(BalanceDto balance);

        string CrearPlantillaPresentacionEmail(BalanceDtoPresentacion balance, string qr);
    }
}
