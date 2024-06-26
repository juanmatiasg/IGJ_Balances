﻿using Balances.DTO;

namespace Balances.Web.Services.Implementation
{
    public interface ICaratulaClientService
    {

        Task<ResponseDTO<BalanceDto>> insertCaratula(CaratulaDto caratulaDto);
        Task<ResponseDTO<BalanceDto>> loadCaratula(string id);


    }
}
