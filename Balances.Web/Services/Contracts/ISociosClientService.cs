using Balances.DTO;
using Newtonsoft.Json.Linq;

namespace Balances.Web.Services.Implementation
{
    public interface ISociosClientService
    {
        Task<ResponseDTO<BalanceDto>> insertPersonaHumana(PersonaHumanaDto personaHumana);

        Task<ResponseDTO<BalanceDto>> insertPersonaJuridica(PersonaJuridicaDto personaJuridica);

        Task<List<JObject>> GetAllCountries();

        Task<List<string>> GetAllProvince();

        Task<ResponseDTO<BalanceDto>> deletePersonaHumana(PersonaHumanaDto personaHumana);

        Task<ResponseDTO<BalanceDto>> deletePersonaJuridica(PersonaJuridicaDto personaJuridica);

    }
}
