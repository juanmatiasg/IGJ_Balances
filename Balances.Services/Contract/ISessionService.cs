﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balances.Services.Contract
{
    public interface ISessionService
    {

        void SetBalanceId(string balanceId);
        string GetBalanceId();
    }


}
