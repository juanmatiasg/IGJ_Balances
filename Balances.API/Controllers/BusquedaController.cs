using Balances.Model;
using BuscarIGJ;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebService;

namespace WebApi.Controllers
{

    [AllowAnonymous]
    [ApiController]
    public class BusquedaController : ControllerBase
    {


        [HttpGet("BusquedaByCuilOrCorrelativo")]
        public async Task<IActionResult> BusquedaByCuilOrCorrelativo(string cuitcorrelativo)
        {
            var entidadService = new WebService.Entidad();

            string filtro = cuitcorrelativo.Replace("-", "").Replace(" ", "");

            if (filtro.Length == 11)
            {
                entidadService = await BusquedaEntidadService.GetByCuit(Convert.ToInt64(filtro));
                EntidadModel oEntidad = new EntidadModel()
                {
                    NroCorrelativo = entidadService.NroCorrelativo.ToString(),
                    TipoEntidad = entidadService.TipoSoc,
                    RazonSocial = entidadService.RazonSocial
                };



                return Ok(oEntidad);
            }
            else
            {
                entidadService = await BusquedaEntidadService.BusquedaEntidadByCorrelativo(Convert.ToInt32(filtro));
                EntidadModel oEntidad = new EntidadModel()
                {
                    NroCorrelativo = entidadService.NroCorrelativo.ToString(),
                    TipoEntidad = entidadService.TipoSoc,
                    RazonSocial = entidadService.RazonSocial
                };



                return Ok(oEntidad);
            }


        }






    }
}