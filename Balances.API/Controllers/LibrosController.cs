using Balances.Bussiness.Contrato;
using Balances.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Balances.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LibrosController : ControllerBase
    {
        private readonly ILibrosBusiness _librosBusiness;

        public LibrosController(ILibrosBusiness librosBusiness)
        {
            _librosBusiness = librosBusiness;
        }

        [HttpPost]
        [Route("InsertLibros")]
        public ResponseDTO<BalanceDto> InsertLibros(LibrosDto libros)
        {

            var balancedto = _librosBusiness.Insert(libros);
            return balancedto;

        }
    }
}
