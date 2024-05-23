using Balances.DTO;

namespace Balances.Services.Contract
{
    public interface IPresentacionService
    {

        string CrearPlantillaPresentacionPdf(BalanceDto balance, string qr);


        //BalanceDtoPresentacion GetBalanceAutoridadySocioFirmante(BalanceDto balance);

        string CrearPlantillaPresentacionEmail(BalanceDto balance, string qr);
    }
}
