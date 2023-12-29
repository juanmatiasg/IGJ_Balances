using Balances.DTO;
using Balances.Web.Services.Implementation;
using System.Net.Http.Json;

namespace Balances.Web.Services.Contracts
{
    public class EstadoContableService : IEstadoContableService
    {
        private readonly HttpClient _httpClient;

        public EstadoContableService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task<ResponseDTO<BalanceDto>> deleteRubro(RubroPatrimonioNetoDto rubroPatrimonioNetoDto)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseDTO<BalanceDto>> getBalance(string id)
        {
            return await _httpClient.GetFromJsonAsync<ResponseDTO<BalanceDto>>($"Balance/{id}");

        }

        public async Task<ResponseDTO<string>> getSession()
        {
            try
            {
                var result = await _httpClient.GetFromJsonAsync<ResponseDTO<string>>($"Session/getSession");

                return new ResponseDTO<string>
                {
                    Result = result.Result,
                    IsSuccess = result.IsSuccess,
                    Message = result.Message
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO<string>
                {
                    Result = null,
                    IsSuccess = false,
                    Message = ex.Message
                };
            }

        }

        public Task<ResponseDTO<BalanceDto>> insertEEC(EstadoContableDto estadoContableDto)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDTO<BalanceDto>> insertRubro(RubroPatrimonioNetoDto rubroPatrimonioNetoDto)
        {
            throw new NotImplementedException();
        }
    }
}
