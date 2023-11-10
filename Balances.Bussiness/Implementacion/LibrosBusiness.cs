using AutoMapper;
using Balances.Bussiness.Contrato;
using Balances.DTO;

namespace Balances.Bussiness.Implementacion
{
    public class LibrosBusiness : ILibrosBusiness
    {
        private readonly IBalanceBusiness _balanceBusiness;
        private readonly IMapper _mapper;

        public LibrosBusiness(IBalanceBusiness balanceBusiness, IMapper mapper)
        {
            _balanceBusiness = balanceBusiness;
            _mapper = mapper;
        }

        public ResponseDTO<BalanceDto> Delete(LibroDto modelo)
        {
            throw new NotImplementedException();
        }

        public ResponseDTO<BalanceDto> Insert(LibrosDto modelo)
        {
            ResponseDTO<BalanceDto> respuesta = new ResponseDTO<BalanceDto>();
            respuesta.IsSuccess = false;

            try
            {

                var bDto = _balanceBusiness.BalanceActual;
                bDto.Libros = modelo;
                _balanceBusiness.Update(bDto);
                respuesta.IsSuccess = true;
                respuesta.Result = bDto;
            }
            catch (Exception ex)
            {

                respuesta.Message = ex.Message;

            }
            return respuesta;
        }
    }
}
