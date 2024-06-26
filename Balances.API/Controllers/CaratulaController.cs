﻿using Balances.Bussiness.Contrato;
using Balances.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Balances.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CaratulaController : ControllerBase
    {

        private readonly ICaratulaBusiness _caratulaBusiness;


        public CaratulaController(ICaratulaBusiness caratulaBusiness)
        {
            _caratulaBusiness = caratulaBusiness;

        }

        [HttpPost("InsertCaratula")]
        public IActionResult Insert(CaratulaDto caratuladto)

        {

            var rsp = _caratulaBusiness.Insert(caratuladto);
            return Ok(rsp);
        }

      


    }
}
