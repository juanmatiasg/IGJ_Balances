using Balances.DTO;

namespace Balances.Services.Contract
{
    public interface IPresentacionService
    {

        string PlantillaPresentacionHtml(BalanceDtoPresentacion balance, string qr);

        BalanceDtoPresentacion GetBalanceAutoridadySocioFirmante(BalanceDto balance);

        string CrearPlantillaPresentacion(BalanceDtoPresentacion balance, string qr);
    }
}
