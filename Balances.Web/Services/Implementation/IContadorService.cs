using Balances.DTO;

namespace Balances.Web.Services.Implementation
{
    public interface IContadorService
    {
        Task<ResponseDTO<ContadorDto>> postContador(string id, string nombre, string tipoDoc, string nroDoc, string nroFiscal, string tomo, string folio, DateTime fechaInformeAuditorExt, string nroLegalInfoAudExt);


        Task<ResponseDTO<BalanceDto>> getBalance(string id);
        

        Task<ResponseDTO<string>> setSession(string idBalance);



    }
}
