using Balances.Bussiness.Contrato;
using Balances.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Balances.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ArchivoController : ControllerBase
    {
        private readonly IArchivoBusiness _archivoBusiness;

        public ArchivoController(IArchivoBusiness archivoBusiness)
        {
            _archivoBusiness = archivoBusiness;
        }

        [HttpPost]
        [Route("InsertArchivos")]
        public ResponseDTO<BalanceDto> InsertArchivos(IFormFileCollection files)
        {

            var rsp = _archivoBusiness.Upload(files);
            return rsp;

        }

        [HttpDelete]
        [Route("DeleteArchivo")]
        public ResponseDTO<BalanceDto> Delete([FromBody] ArchivoDTO archivodto)
        {

            var rsp = _archivoBusiness.Delete(archivodto);
            return rsp;

        }




    }
}
