﻿using Balances.DTO;

namespace Balances.Web.Services.Implementation
{
    public interface IContadorClientService
    {
        Task<ResponseDTO<BalanceDto>> postContador(ContadorDto contador);

        Task<ResponseDTO<BalanceDto>> getBalance(string id);

        Task<ResponseDTO<string>> getSession();



    }
}