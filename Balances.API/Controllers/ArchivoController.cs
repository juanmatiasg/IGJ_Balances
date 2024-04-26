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
        public ResponseDTO<BalanceDto> InsertArchivos([FromBody] List<ArchivoDTO> files)
        {

            var rsp = _archivoBusiness.UploadFilesDTO(files);
            return rsp;

        }

        [HttpDelete]
        [Route("DeleteArchivo")]
        public ResponseDTO<BalanceDto> Delete([FromBody] ArchivoDTO file)
        {

            var rsp = _archivoBusiness.Delete(file);
            return rsp;

        }




    }
}
