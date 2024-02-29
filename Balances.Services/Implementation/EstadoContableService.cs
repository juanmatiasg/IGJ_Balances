using Balances.DTO;
using Balances.Model;
using Balances.Services.Contract;

namespace Balances.Services.Implementation
{
    public class EstadoContableService : IEstadoContableService
    {
        private readonly IBalanceService _balanceService;

        public EstadoContableService(IBalanceService balanceService)
        {
            _balanceService = balanceService;
        }

        public EstadoContable ActualizarEstadoContable(ActualizarEstadoContableDto estadoContable)
        {
            var balance = _balanceService.GetById(estadoContable.balanceId);

             balance.EstadoContable = estadoContable.estadoContable.GetEstadoContable();
             balance.EstadoContable.OtrosRubros = estadoContable.estadoContable.GetEstadoContable().OtrosRubros;
            //balance.EstadoContable = estadoContable.estadoContable


            //balance.EstadoContable.Estado = _validation.GetEstado(balance.EstadoContable);
           // balance.EstadoContable.OtrosRubros = estadoContable;
            _balanceService.UpdateBalance(balance);

            return balance.EstadoContable;
        }

        public EstadoContable Get(string balanceId)
        {

            var balance = _balanceService.GetById(balanceId);

            return balance.EstadoContable;
        }
    }
}

