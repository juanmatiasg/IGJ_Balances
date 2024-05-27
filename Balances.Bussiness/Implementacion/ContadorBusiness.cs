using Balances.Bussiness.Contrato;
using Balances.DTO;
using Balances.Model;
using Balances.Services.Contract;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Balances.Bussiness.Implementacion
{
    public class ContadorBusiness : IContadorBusiness

    {


        private readonly ISessionService _sessionService;

        private readonly IBalanceBusiness _balanceBusiness;

        private readonly ILogger<ContadorBusiness> _logger;


        public ContadorBusiness(ISessionService sessionService,
                               IBalanceBusiness balanceBusiness,

                               ILogger<ContadorBusiness> logger)
        {
            _sessionService = sessionService;
            _balanceBusiness = balanceBusiness;

            _logger = logger;

        }



        public ResponseDTO<BalanceDto> Delete(ContadorDto modelo)
        {

            throw new NotImplementedException();
        }

        public ResponseDTO<BalanceDto> Insert(ContadorDto modelo)
        {
            ResponseDTO<BalanceDto> respuesta = new ResponseDTO<BalanceDto>();
            respuesta.IsSuccess = false;
            var contadorSerializado = JsonConvert.SerializeObject(modelo);
            try
            {
                var id = _sessionService.GetBalanceId(modelo.SesionId);

                var responsedto = _balanceBusiness.GetById(id);

                if (responsedto.IsSuccess)
                {
                    var balance = responsedto.Result;
                    //mapeamos el contador al balance 

                    balance.Contador = MappToContador(modelo);

                    var rsp = _balanceBusiness.Update(balance);

                    // si inserto correctamente
                    if (rsp.IsSuccess)
                    {

                        respuesta.IsSuccess = true;
                        respuesta.Message = "Contador generado correctamente:";
                        _logger.LogInformation($"ContadorBusiness.Insert: --> {contadorSerializado}");
                        respuesta.Result = balance;

                    }

                }
                else
                    respuesta.Message = "No se encontro el balance";
            }
            catch (Exception ex)
            {
                respuesta.Message = ex.Message;
                _logger.LogError($"ContadorBusiness.Insert: \n {ex}");
            }

            return respuesta;
        }

        public IEnumerable<ContadorDto> List()
        {
            throw new NotImplementedException();
        }

        public bool Update(ContadorDto modelo)
        {
            throw new NotImplementedException();
        }

        private ContadorDto MappToContadorDto(Contador modelo)
        {
            var contadordto = new ContadorDto()
            {
                NroDocumento = modelo.NroDocumento,
                Apellido = modelo.Apellido,
                FechaInformeAuditorExt = modelo.FechaInformeAuditorExt,
                Folio = modelo.Folio,
                Nombre = modelo.Nombre,
                NroFiscal = modelo.NroFiscal,
                TipoDocumento = modelo.TipoDocumento,
                NroLegalInfoAudExt = modelo.NroLegalInfoAudExt,
                Tomo = modelo.Tomo,
                Opinion = modelo.Opinion,
                Observaciones = modelo.Observaciones,
                TomoEstudio = modelo.TomoEstudio,
                FolioEstudio = modelo.FolioEstudio,
                EsSocioEstudio = modelo.EsSocioEstudio


            };

            return contadordto;
        }

        public ContadorDto GetById(string id)
        {

            try
            {
                var balance = _balanceBusiness.GetById(id);

                var contadordto = MappToContadorDto(balance.Result.Contador);

                return contadordto;

            }
            catch (Exception ex)
            {

                _logger.LogError($"ContadorBusiness.GetById: \n {ex}");
                throw ex;
            }


        }

        private Contador MappToContador(ContadorDto modelo)
        {
            var contador = new Contador()
            {
                Apellido = modelo.Apellido,
                Nombre = modelo.Nombre,
                NroDocumento = modelo.NroDocumento,
                NroFiscal = modelo.NroFiscal,
                FechaInformeAuditorExt = (DateTime)modelo.FechaInformeAuditorExt,
                Folio = modelo.Folio,
                NroLegalInfoAudExt = modelo.NroLegalInfoAudExt,
                TipoDocumento = modelo.TipoDocumento,
                Tomo = modelo.Tomo,
                Opinion = modelo.Opinion,
                Observaciones = modelo.Observaciones,
                TomoEstudio = modelo.TomoEstudio,
                FolioEstudio = modelo.FolioEstudio,
                EsSocioEstudio = modelo.EsSocioEstudio
            };

            return contador;
        }





    }
}
