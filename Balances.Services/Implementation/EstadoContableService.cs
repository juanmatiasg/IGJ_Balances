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
            //var otrosRubros = balance.EstadoContable.OtrosRubros;
            balance.EstadoContable = estadoContable.estadoContable.GetEstadoContable();


            //balance.EstadoContable.Estado = _validation.GetEstado(balance.EstadoContable);
            //balance.EstadoContable.OtrosRubros = otrosRubros;
            _balanceService.UpdateBalance(balance.Id, balance);

            return balance.EstadoContable;
        }

        public EstadoContable Get(string balanceId)
        {

            var balance = _balanceService.GetById(balanceId);

            return balance.EstadoContable;
        }
    }
}
