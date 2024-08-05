using Balances.Bussiness.Contrato;
using Balances.DTO;
using Balances.Services.Contract;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Balances.Bussiness.Implementacion
{
    public class AutoridadesBusiness : IAutoridadesBusiness
    {
        private readonly ISessionService _sessionService;
        private readonly IBalanceBusiness _balanceBusiness;

        private readonly ILogger<AutoridadesBusiness> _logger;


        public AutoridadesBusiness(ISessionService sessionService,
                                   IBalanceBusiness balanceBusiness,
                                   ILogger<AutoridadesBusiness> logger)
        {
            _sessionService = sessionService;
            _balanceBusiness = balanceBusiness;

            _logger = logger;
        }

        public ResponseDTO<BalanceDto> Delete(AutoridadDto modelo)
        {
            var autoridadSerializada = JsonConvert.SerializeObject(modelo);
            var resultadoDto = new ResponseDTO<BalanceDto>();
            resultadoDto.IsSuccess = false;
            try
            {
                //RECUPERO BALANCE 
                //var bal = _balanceBusiness.BalanceActual;
                var id = _sessionService.GetBalanceId(modelo.SesionId);
                var rst = _balanceBusiness.GetById(id);
                var bal = rst.Result;
                //BUSCO AUTORIDAD
                var autoridad = bal.Autoridades.FirstOrDefault(x => x.Id == modelo.Id);

                // SI LA ENCUENTRO LA ELIMINO
                if (autoridad != null) bal.Autoridades.Remove(autoridad);

                _balanceBusiness.Update(bal);

                _logger.LogInformation($"AutoridadesBusiness.Delete: --> {autoridadSerializada}");
                resultadoDto.IsSuccess = true;
                resultadoDto.Message = "Autoridad eliminada correctamente";
                resultadoDto.Result = bal;
            }
            catch (Exception ex)
            {
                _logger.LogError($"AutoridadesBusiness.Delete: \n {ex}");
                resultadoDto.Message = ex.Message;
            }

            return resultadoDto;
        }

        public ResponseDTO<BalanceDto> Update(AutoridadDto modelo)
        {
            var autoridadSerializada = JsonConvert.SerializeObject(modelo);
            var resultadoDto = new ResponseDTO<BalanceDto>();
            resultadoDto.IsSuccess = false;
            try
            {
                //RECUPERO BALANCE 
                //var bal = _balanceBusiness.BalanceActual;
                var id = _sessionService.GetBalanceId(modelo.SesionId);
                var rst = _balanceBusiness.GetById(id);
                var bal = rst.Result;
                //BUSCO AUTORIDAD
                var autoridad = bal.Autoridades.FirstOrDefault(x => x.Id == modelo.Id);

                // SI LA ENCUENTRO LA ELIMINO Y ACTUALIZO
                if (autoridad != null)
                {
                    bal.Autoridades.Remove(autoridad);
                    bal.Autoridades.Add(modelo);
                        };

                _balanceBusiness.Update(bal);

                _logger.LogInformation($"AutoridadesBusiness.Delete: --> {autoridadSerializada}");
                resultadoDto.IsSuccess = true;
                resultadoDto.Message = "Autoridad Actualizada correctamente";
                resultadoDto.Result = bal;
            }
            catch (Exception ex)
            {
                _logger.LogError($"AutoridadesBusiness.Delete: \n {ex}");
                resultadoDto.Message = ex.Message;
            }

            return resultadoDto;
        }


        public ResponseDTO<BalanceDto> Insert(AutoridadDto modelo)
        {
            ResponseDTO<BalanceDto> respuesta = new ResponseDTO<BalanceDto>();
            respuesta.IsSuccess = false;
            var autoridadSerializada = JsonConvert.SerializeObject(modelo);
            try
            {
                //var sesionId = _sessionService.GetNewSesion();
                //var id = _sessionService.GetBalanceId(sesionId);
                var id = _sessionService.GetBalanceId(modelo.SesionId);

                var resultadoDto = _balanceBusiness.GetById(id);

                if (resultadoDto.IsSuccess)
                {
                    var balanceDto = resultadoDto.Result;

                    modelo.Id = Guid.NewGuid().ToString();

                    if (balanceDto.Autoridades == null)
                        balanceDto.Autoridades = new List<AutoridadDto>();

                    balanceDto.Autoridades.Add(modelo);

                    var rsp = _balanceBusiness.Update(balanceDto);
                    _logger.LogInformation($"AutoridadesBusiness.Insert --> {autoridadSerializada}");
                    respuesta = rsp;
                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"AutoridadesBusiness.Insert: \n {ex}");
                respuesta.Message = ex.Message;
            }

            return respuesta;
        }


    }


}
