using AutoMapper;
using Balances.Bussiness.Contrato;
using Balances.DTO;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Balances.Bussiness.Implementacion
{
    public class LibrosBusiness : ILibrosBusiness
    {
        private readonly IBalanceBusiness _balanceBusiness;
        private readonly IMapper _mapper;
        private readonly ILogger<LibrosBusiness> _logger;

        public LibrosBusiness(IBalanceBusiness balanceBusiness,
                             IMapper mapper,
                             ILogger<LibrosBusiness> logger)
        {
            _balanceBusiness = balanceBusiness;
            _mapper = mapper;
            _logger = logger;
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

                var bDto = _balanceBusiness.BalanceActual;
                bDto.Libros = modelo;
                _balanceBusiness.Update(bDto);
                respuesta.IsSuccess = true;
                respuesta.Result = bDto;
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
