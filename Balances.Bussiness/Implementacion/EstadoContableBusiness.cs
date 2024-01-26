using AutoMapper;
using Balances.Bussiness.Contrato;
using Balances.DTO;
using Balances.Model;
using Balances.Services.Contract;

namespace Balances.Bussiness.Implementacion
{
    public class EstadoContableBusiness : IEstadoContableBusiness
    {

        private readonly ISessionService _sessionService;
        private readonly IBalanceBusiness _balanceBusiness;
        private readonly IMapper _mapper;

        public EstadoContableBusiness(ISessionService sessionService,
                                      IBalanceBusiness balanceBusiness,
                                      IMapper mapper)
        {
            _sessionService = sessionService;
            _balanceBusiness = balanceBusiness;
            _mapper = mapper;
        }

        public ResponseDTO<BalanceDto> Delete(RubroPatrimonioNetoDto modelo)
        {
            var resultadoDto = new ResponseDTO<BalanceDto>();
            resultadoDto.IsSuccess = false;

            try
            {
                // RECUPERO EL BALANCE ACTUAL
                var bDto = _balanceBusiness.BalanceActual;

                //BUSCO EL OTRO RUBRO A BORRAR
                var rubro = bDto.EstadoContable.otrosRubros.FirstOrDefault(p => p.Codigo == modelo.codigo);

                if (rubro != null)
                {
                    bDto.EstadoContable.otrosRubros.Remove(rubro);
                }

                //ACTUALIZO LA DB
                var rst = _balanceBusiness.Update(bDto);

                resultadoDto = rst;


            }
            catch (Exception ex)
            {
                resultadoDto.Message = ex.Message;

            }
            return resultadoDto;

        }

        public ResponseDTO<BalanceDto> Insert(EstadoContableDto modelo)
        {
            ResponseDTO<BalanceDto> respuesta = new ResponseDTO<BalanceDto>();
            respuesta.IsSuccess = false;

            try
            {
                var id = _sessionService.GetBalanceId();
                var resultadoDto = _balanceBusiness.GetById(id);

                if (resultadoDto.IsSuccess)
                {
                   var balanceDto = resultadoDto.Result;
     
                    balanceDto.EstadoContable = _mapper.Map<EstadoContable>(modelo);


                    var rsp = _balanceBusiness.Update(balanceDto);

                    respuesta = rsp;
                }

            
            }
            catch (Exception ex)
            {
                respuesta.Message = ex.Message;
            }

            return respuesta;

        }


        public ResponseDTO<BalanceDto> Insert(RubroPatrimonioNetoDto modelo)
        {
            ResponseDTO<BalanceDto> respuesta = new ResponseDTO<BalanceDto>();
            respuesta.IsSuccess = false;

            try
            {
                var id = _sessionService.GetBalanceId();
                var resultadoDto = _balanceBusiness.GetById(id);

                if (resultadoDto.IsSuccess)
                {
                    var balanceDto = resultadoDto.Result;

                    var rubro = _mapper.Map<RubroPatrimonioNeto>(modelo);
                    rubro.Codigo = Guid.NewGuid().ToString();
                    balanceDto.EstadoContable.otrosRubros.Add(rubro);
                    var rsp = _balanceBusiness.Update(balanceDto);

                    respuesta = rsp;
                }

          
            }
            catch (Exception ex)
            {
                respuesta.Message = ex.Message;
            }

            return respuesta;

        }

       
    }
}
