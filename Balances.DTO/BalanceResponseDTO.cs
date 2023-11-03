using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balances.DTO
{
    public class BalanceResponseDTO
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public EntidadResponseDTO Entidad { get; set; }
    }
}
