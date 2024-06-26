﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balances.DTO
{
    public class EntidadResponseDTO
    {
        public string RazonSocial { get; set; }
        public string TipoEntidad { get; set; }
        public string Domicilio { get; set; }
        public bool SedeSocialInscripta { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaDeCierre { get; set; }
    }
}
