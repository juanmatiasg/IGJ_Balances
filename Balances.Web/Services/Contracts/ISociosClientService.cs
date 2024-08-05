using Balances.DTO;
using Newtonsoft.Json.Linq;

namespace Balances.Web.Services.Implementation
{
    public interface ISociosClientService
    {
        Task<ResponseDTO<BalanceDto>> insertPersonaHumana(PersonaHumanaDto personaHumana);
        Task<ResponseDTO<BalanceDto>> updatePersonaHumana(PersonaHumanaDto personaHumana);
        Task<ResponseDTO<BalanceDto>> deletePersonaHumana(PersonaHumanaDto personaHumana);


        Task<ResponseDTO<BalanceDto>> insertPersonaJuridica(PersonaJuridicaDto personaJuridica);
        Task<ResponseDTO<BalanceDto>> updatePersonaJuridica(PersonaJuridicaDto personaJuridica);
        Task<ResponseDTO<BalanceDto>> deletePersonaJuridica(PersonaJuridicaDto personaJuridica);

        Task<List<string>> GetAllCountries();
        Task<List<string>> GetAllProvince();


    }
}
