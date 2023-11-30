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
        public async Task<IActionResult> BusquedaByCuilOrCorrelativo(string nroCorrelativo)
        {
            try
            {
                var entidadService = new WebService.Entidad();

         

                if (!string.IsNullOrEmpty(nroCorrelativo))
                {
                    string filtro = nroCorrelativo.Replace("-", "").Replace(" ", "");

                    if (filtro.Length == 11)
                    {

                        var response = new ResponseDTO<BusquedaEntidadResponse>
                        {
                            Result = new BusquedaEntidadResponse
                            {
                                NroCorrelativo = entidadService.NroCorrelativo.ToString(),
                                TipoEntidad = entidadService.TipoSoc,
                                RazonSocial = entidadService.RazonSocial
                            },
                            IsSuccess = true,
                            Message = "Entidad encontrada"
                        };

                        return Ok(response);

                    }
                    else
                    {
                        entidadService = await BusquedaEntidadService.BusquedaEntidadByCorrelativo(Convert.ToInt32(filtro));
                        var response = new ResponseDTO<BusquedaEntidadResponse>
                        {
                            Result = new BusquedaEntidadResponse
                            {
                                NroCorrelativo = entidadService.NroCorrelativo.ToString(), 
                                TipoEntidad = entidadService.TipoSoc,
                                RazonSocial = entidadService.RazonSocial
                            },
                            IsSuccess = true,
                            Message = "Entidad encontrada"
                        };

                        return Ok(response);



                    }
                }
                else
                {
                    return BadRequest("El parámetro cuitcorrelativo es nulo o vacío.");
                }
            }
            catch (Exception ex) {
                Console.WriteLine($"Error en la acción BusquedaByCuilOrCorrelativo: {ex.Message}");
                return StatusCode(500, "Error interno del servidor");
            }

        }

    }
}