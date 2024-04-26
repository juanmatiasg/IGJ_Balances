using AutoMapper;
using Balances.Bussiness.Contrato;
using Balances.DTO;
using Balances.Services.Contract;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Balances.Bussiness.Implementacion
{
    public class LibrosBusiness : ILibrosBusiness
    {
        private readonly IBalanceBusiness _balanceBusiness;
        private readonly ISessionService _sessionService;
        private readonly IMapper _mapper;
        private readonly ILogger<LibrosBusiness> _logger;

        public LibrosBusiness(IBalanceBusiness balanceBusiness,
                             IMapper mapper,
                             ILogger<LibrosBusiness> logger,
                             ISessionService sessionService)
        {
            _balanceBusiness = balanceBusiness;
            _mapper = mapper;
            _logger = logger;
            _sessionService = sessionService;
        }

        public ResponseDTO<BalanceDto> Delete(LibroDto modelo)
        {
            throw new NotImplementedException();
        }

        public ResponseDTO<BalanceDto> Insert(LibrosDto modelo)
        {
            ResponseDTO<BalanceDto> respuesta = new ResponseDTO<BalanceDto>();
            respuesta.IsSuccess = false;
            var libroSerializado = JsonConvert.SerializeObject(modelo);
            try
            {
                var id = _sessionService.GetBalanceId(modelo.SessionId);

                var bDto = _balanceBusiness.GetById(id);
                //var bDto = _balanceBusiness.BalanceActual;
                bDto.Result.Libros = modelo;
                _balanceBusiness.Update(bDto.Result);
                respuesta.IsSuccess = true;
                respuesta.Result = bDto.Result;
                _logger.LogInformation($"LibrosBusiness.Insert: --> {libroSerializado}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"LibrosBusiness.Insert: {ex}");
                respuesta.Message = ex.Message;

            }
            return respuesta;
        }
    }
}
