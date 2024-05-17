using AutoMapper;
using Balances.Bussiness.Contrato;
using Balances.DTO;
using Balances.Model;
using Balances.Services.Contract;
using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

namespace Balances.Bussiness.Implementacion
{
    public class EstadoContableBusiness : IEstadoContableBusiness
    {

        private readonly ISessionService _sessionService;
        private readonly IBalanceBusiness _balanceBusiness;
        private readonly IMapper _mapper;
        private readonly ILogger<EstadoContableBusiness> _logger;


        public EstadoContableBusiness(ISessionService sessionService,
                                      IBalanceBusiness balanceBusiness,
                                      IMapper mapper,
                                      ILogger<EstadoContableBusiness> logger)
        {
            _sessionService = sessionService;
            _balanceBusiness = balanceBusiness;
            _mapper = mapper;
            _logger = logger;
        }


        public ResponseDTO<BalanceDto> InsertEECC(EstadoContableDto modelo)
        {
            ResponseDTO<BalanceDto> respuesta = new ResponseDTO<BalanceDto>();
            respuesta.IsSuccess = false;
            var EECCSerializado = JsonConvert.SerializeObject(modelo);
            try
            {
                var id = _sessionService.GetBalanceId(modelo.SesionId);

                var resultadoDto = _balanceBusiness.GetById(id);



                if (resultadoDto.IsSuccess)
                {
                    var balanceDto = resultadoDto.Result;


                    balanceDto.EstadoContable = _mapper.Map<EstadoContable>(modelo);

                    var rsp = _balanceBusiness.Update(balanceDto);

                    _logger.LogInformation($"EstadoContableBusiness.Insert EECC --> {EECCSerializado} ");
                    respuesta = rsp;
                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"EstadoContableBusiness.Insert: \n {ex}");
                respuesta.Message = ex.Message;
            }

            return respuesta;

        }


        public ResponseDTO<BalanceDto> InsertRubro(RubroPatrimonioNetoDto modelo)
        {
            ResponseDTO<BalanceDto> respuesta = new ResponseDTO<BalanceDto>();
            respuesta.IsSuccess = false;
            var rubroSerializado = JsonConvert.SerializeObject(modelo);
            try
            {


                var id = _sessionService.GetBalanceId(modelo.SesionId);

                var resultadoDto = _balanceBusiness.GetById(id);



                if (resultadoDto.IsSuccess)
                {
                    var balanceDto = resultadoDto.Result;

                    var estadoContable = balanceDto.EstadoContable;

                    var rubro = _mapper.Map<RubroPatrimonioNeto>(modelo);

                    estadoContable.OtrosRubros.Add(rubro);


                    //Actualiza el balance

                    var rsp = _balanceBusiness.Update(balanceDto);

                    _logger.LogInformation($"EstadoContableBusiness.Insert rubro --> {rubroSerializado} ");
                    respuesta = rsp;
                }


            }
            catch (Exception ex)
            {
                respuesta.Message = ex.Message;
                _logger.LogError($"EstadoContableBusiness.Insert modelo RubroPatrimonioNeto: \n {ex}");
            }

            return respuesta;

        }

        public ResponseDTO<BalanceDto> DeleteRubro(RubroPatrimonioNetoDto modelo)
        {

            var resultadoDto = new ResponseDTO<BalanceDto>();
            resultadoDto.IsSuccess = false;

            var rubroSerializado = JsonConvert.SerializeObject(modelo);
            try
            {
                // RECUPERO EL BALANCE ACTUAL
                var balanceId = _sessionService.GetBalanceId(modelo.SesionId);
                var bDto = _balanceBusiness.GetById(balanceId);
                //var bDto = _balanceBusiness.BalanceActual;

                //BUSCO EL OTRO RUBRO A BORRAR
                var rubro = bDto.Result.EstadoContable.OtrosRubros.FirstOrDefault(p => p.Codigo == modelo.codigo);

                if (rubro != null)
                {
                    bDto.Result.EstadoContable.OtrosRubros.Remove(rubro);
                }

                //ACTUALIZO LA DB
                var rst = _balanceBusiness.Update(bDto.Result);

                resultadoDto = rst;

                _logger.LogInformation($"EstadoContableBusiness.Insert rubro  --> {rubroSerializado}");
            }
            catch (Exception ex)
            {
                resultadoDto.Message = ex.Message;
                _logger.LogError($"EstadoContableBusiness.Delete rubro: \n {ex}");

            }
            return resultadoDto;

        }



    }
}
