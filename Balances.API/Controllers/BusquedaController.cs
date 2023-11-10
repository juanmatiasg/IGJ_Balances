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
        public async Task<IActionResult> BusquedaByCuilOrCorrelativo(string cuitcorrelativo)
        {
            var entidadService = new WebService.Entidad();
            string filtro = cuitcorrelativo.Replace("-", "").Replace(" ", "");

            BusquedaEntidadResponse response;

            if (filtro.Length == 11)
            {
                entidadService = await BusquedaEntidadService.GetByCuit(Convert.ToInt64(filtro));
                response = new BusquedaEntidadResponse
                {
                    NroCorrelativo = entidadService.NroCorrelativo.ToString(),
                    TipoEntidad = entidadService.TipoSoc,
                    RazonSocial = entidadService.RazonSocial
                    // Populate other properties as needed
                };
            }
            else
            {
                entidadService = await BusquedaEntidadService.BusquedaEntidadByCorrelativo(Convert.ToInt32(filtro));
                response = new BusquedaEntidadResponse
                {
                    NroCorrelativo = entidadService.NroCorrelativo.ToString(),
                    TipoEntidad = entidadService.TipoSoc,
                    RazonSocial = entidadService.RazonSocial
                    // Populate other properties as needed
                };
            }

            return Ok(response);
        }
    }
}