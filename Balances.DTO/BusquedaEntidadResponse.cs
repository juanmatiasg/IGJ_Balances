﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balances.DTO
{
    public  class BusquedaEntidadResponse
    {
        public string? Id { get; set; }
        public string? RazonSocial { get; set; }
        public string? NroCorrelativo { get; set; }
        public string? Cuit { get; set; }
        public string? TipoEntidad { get; set; }
        public string? Estado { get; set; }
    }
}
