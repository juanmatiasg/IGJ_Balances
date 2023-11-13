using Balances.DTO;
using Balances.Model;
using BuscarIGJ;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebService;

namespace WebApi.Controllers
{ //sesion y objeto

    [AllowAnonymous]
    [ApiController]
    public class BusquedaController : ControllerBase
    {

      
            [HttpGet("BusquedaByCuilOrCorrelativo")]
            public async Task<IActionResult> BusquedaByCuilOrCorrelativo(string cuitOrCorrelativo)
            {
                var entidadService = new WebService.Entidad();

                string filtro = SanitizeInput(cuitOrCorrelativo);

                if (TryParseCuit(filtro, out long cuit))
                {
                    entidadService = await BusquedaEntidadService.GetByCuit(cuit);
                }
                else if (TryParseCorrelativo(filtro, out int correlativo))
                {
                    entidadService = await BusquedaEntidadService.BusquedaEntidadByCorrelativo(correlativo);
                }
                else
                {
                    return BadRequest("Invalid input. Please provide a valid CUIT (11 characters) or correlativo.");
                }

                if (entidadService == null)
                {
                    return NotFound();
                }

                var response = new ResponseDTO<WebService.Entidad>
                {
                    Result = entidadService,
                    IsSuccess = true,
                    Message = "Entity found"
                };

                return Ok(response);
            }

            private string SanitizeInput(string input)
            {
                return input.Replace("-", "").Replace(" ", "");
            }

            private bool TryParseCuit(string input, out long cuit)
            {
                cuit = 0;
                return input.Length == 11 && long.TryParse(input, out cuit);
            }

            private bool TryParseCorrelativo(string input, out int correlativo)
            {
                correlativo = 0;
                return int.TryParse(input, out correlativo);
            }
        }
      
    
}