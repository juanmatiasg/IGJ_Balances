using Balances.DTO;

namespace Balances.Web.Services.Implementation
{
    public interface ISociosClientService
    {
        Task<ResponseDTO<BalanceDto>> insertPersonaHumana(PersonaHumanaDto personaHumana);

        Task<ResponseDTO<BalanceDto>> insertPersonaJuridica(PersonaJuridicaDto personaJuridica);


        Task<ResponseDTO<BalanceDto>> deletePersonaHumana(PersonaHumanaDto personaHumana);

        Task<ResponseDTO<BalanceDto>> deletePersonaJuridica(PersonaJuridicaDto personaJuridica);


        Task<ResponseDTO<BalanceDto>> getBalance(string id);

        Task<ResponseDTO<string>> getSession();
    }
}
