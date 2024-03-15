using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balances.Model
{
    public class BalanceToken
    {
        public string SessionId { get; set; }
        public string BalanceId { get; set; }
        public BalanceToken() { }
    }
}
