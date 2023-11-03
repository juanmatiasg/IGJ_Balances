using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balances.DTO
{
    public class BalanceRequestDTO
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public EntidadRequestDTO Entidad { get; set; }
    }
}
