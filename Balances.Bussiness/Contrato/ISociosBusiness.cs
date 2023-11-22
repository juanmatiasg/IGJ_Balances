using Balances.DTO;

namespace Balances.Bussiness.Contrato
{
    public interface ISociosBusiness
    {
        ResponseDTO<BalanceDto> InsertPersonaHumana(PersonaHumanaDto modelo);
        ResponseDTO<BalanceDto> InsertPersonaJuriridica(PersonaJuridicaDto modelo);
    }
}
