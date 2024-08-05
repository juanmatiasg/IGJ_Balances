using Balances.DTO;

namespace Balances.Bussiness.Contrato
{
    public interface ISociosBusiness
    {
        ResponseDTO<BalanceDto> InsertPersonaHumana(PersonaHumanaDto modelo);
        ResponseDTO<BalanceDto> InsertPersonaJuriridica(PersonaJuridicaDto modelo);
        ResponseDTO<BalanceDto> UpdatePersonaHumana(PersonaHumanaDto modelo);
        ResponseDTO<BalanceDto> UpdatePersonaJuridica(PersonaJuridicaDto modelo);

        ResponseDTO<BalanceDto> DeletePersonaHumana(PersonaHumanaDto modelo);
        ResponseDTO<BalanceDto> DeletePersonaJuridica(PersonaJuridicaDto modelo);
    }
}
