using AutoMapper;
using Balances.Bussiness.Contrato;
using Balances.DTO;
using Balances.Services.Contract;
using Balances.Utilities;

namespace Balances.Bussiness.Implementacion
{
    public class AutoridadesBusiness : IAutoridadesBusiness
    {
        private readonly ISessionService _sessionService;
        private readonly IBalanceBusiness _balanceBusiness;
        private readonly IMapper _mapper;


        public AutoridadesBusiness(ISessionService sessionService,
                                   IBalanceBusiness balanceBusiness,
                                   IMapper mapper)
        {
            _sessionService = sessionService;
            _balanceBusiness = balanceBusiness;
            _mapper = mapper;
        }

        public ResponseDTO<BalanceDto> Delete(AutoridadDto modelo)
        {
            var resultadoDto = new ResponseDTO<BalanceDto>();
            resultadoDto.IsSuccess = false;
            try
            {
                //RECUPERO BALANCE 
                var bal = _balanceBusiness.BalanceActual;

                //BUSCO AUTORIDAD
                var autoridad = bal.Autoridades.FirstOrDefault(x => x.Id == modelo.Id);

                // SI LA ENCUENTRO LA ELIMINO
                if (autoridad != null) bal.Autoridades.Remove(autoridad);

                _balanceBusiness.Update(bal);

                resultadoDto.IsSuccess = true;
                resultadoDto.Message = "Autoridad eliminada correctamente";
                resultadoDto.Result = bal;
            }
            catch (Exception ex)
            {

                resultadoDto.Message = ex.Message;
            }

            return resultadoDto;
        }


        public ResponseDTO<BalanceDto> Insert(AutoridadDto modelo)
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

                    modelo.Id = Guid.NewGuid().ToString();

                    if (balanceDto.Autoridades == null)
                        balanceDto.Autoridades = new List<AutoridadDto>();

                    balanceDto.Autoridades.Add(modelo);

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

        public bool Update(AutoridadesLegalesDto modelo)
        {
            throw new NotImplementedException();
        }
    }


}
