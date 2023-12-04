using AutoMapper;
using Balances.Bussiness.Contrato;
using Balances.DTO;
using Balances.Model;
using Balances.Services.Contract;

namespace Balances.Bussiness.Implementacion
{
    public class EstadoContableBusiness : IEstadoContableBusiness
    {

        private readonly ISessionService _sessionService;
        private readonly IBalanceBusiness _balanceBusiness;
        private readonly IMapper _mapper;

        public EstadoContableBusiness(ISessionService sessionService,
                                      IBalanceBusiness balanceBusiness,
                                      IMapper mapper)
        {
            _sessionService = sessionService;
            _balanceBusiness = balanceBusiness;
            _mapper = mapper;
        }

        public ResponseDTO<BalanceDto> Delete(RubroPatrimonioNetoDto modelo)
        {
            var resultadoDto = new ResponseDTO<BalanceDto>();
            resultadoDto.IsSuccess = false;

            try
            {
                // RECUPERO EL BALANCE ACTUAL
                var bDto = _balanceBusiness.BalanceActual;

                //BUSCO EL OTRO RUBRO A BORRAR
                var rubro = bDto.EstadoContable.otrosRubros.FirstOrDefault(p => p.Codigo == modelo.codigo);

                if (rubro != null)
                {
                    bDto.EstadoContable.otrosRubros.Remove(rubro);
                }

                //ACTUALIZO LA DB
                var rst = _balanceBusiness.Update(bDto);

                resultadoDto = rst;


            }
            catch (Exception ex)
            {
                resultadoDto.Message = ex.Message;

            }
            return resultadoDto;

        }

        public ResponseDTO<BalanceDto> Insert(EstadoContableDto modelo)
        {
            ResponseDTO<BalanceDto> respuesta = new ResponseDTO<BalanceDto>();
            respuesta.IsSuccess = false;

            try
            {
                var id = _sessionService.GetBalanceId();
                var resultadoDto = _balanceBusiness.GetById(id);

                if (resultadoDto.IsSuccess)
                {
                    var balanceDto = resultadoDto.Result;



                    if (balanceDto.EstadoContable.otrosRubros == null)
                        balanceDto.EstadoContable.otrosRubros = new List<RubroPatrimonioNeto>();


                    foreach (var rubro in balanceDto.EstadoContable.otrosRubros.ToList())
                    {

                        rubro.Codigo = Guid.NewGuid().ToString();
                        balanceDto.EstadoContable.otrosRubros.Add(rubro);
                        //balanceDto.EstadoContable.otrosRubros.AddRange(rubro);
                    }

                    //balanceDto.EstadoContable.otrosRubros.Add(rubro);

                    var rsp = _balanceBusiness.Update(balanceDto);

                    respuesta = rsp;
                }

                //if (resultadoDto.IsSuccess)
                //{
                //    var balanceDto = resultadoDto.Result;

                //    /*ACTUALIZAR BALANCE CON EL DTO*/
                //    balanceDto.EstadoContable = _mapper.Map<EstadoContable>(modelo);

                //    if (balanceDto.EstadoContable.otrosRubros == null)
                //        balanceDto.EstadoContable.otrosRubros = new List<RubroPatrimonioNeto>();

                //    foreach (var rubro in balanceDto.EstadoContable.otrosRubros.ToList())
                //    {
                //        rubro.Codigo = Guid.NewGuid().ToString();
                //        balanceDto.EstadoContable.otrosRubros.Add(rubro);
                //    }

                //    var rsp = _balanceBusiness.Update(balanceDto);

                //    respuesta = rsp;
                //}
            }
            catch (Exception ex)
            {
                respuesta.Message = ex.Message;
            }

            return respuesta;

        }


        public ResponseDTO<BalanceDto> Insert(RubroPatrimonioNetoDto modelo)
        {
            ResponseDTO<BalanceDto> respuesta = new ResponseDTO<BalanceDto>();
            respuesta.IsSuccess = false;

            try
            {
                var id = _sessionService.GetBalanceId();
                var resultadoDto = _balanceBusiness.GetById(id);

                if (resultadoDto.IsSuccess)
                {
                    var balanceDto = resultadoDto.Result;



                    if (balanceDto.EstadoContable.otrosRubros == null)
                        balanceDto.EstadoContable.otrosRubros = new List<RubroPatrimonioNeto>();


                    /*foreach (var rubro in balanceDto.EstadoContable.otrosRubros.ToList())
                    {

                        rubro.Codigo = Guid.NewGuid().ToString();
                        balanceDto.EstadoContable.otrosRubros.Add(rubro);
                        //balanceDto.EstadoContable.otrosRubros.AddRange(rubro);
                    }*/
                    var rubro = _mapper.Map<RubroPatrimonioNeto>(modelo);
                    rubro.Codigo = Guid.NewGuid().ToString();
                    balanceDto.EstadoContable.otrosRubros.Add(rubro);

                    //balanceDto.EstadoContable.otrosRubros.Add(rubro);

                    var rsp = _balanceBusiness.Update(balanceDto);

                    respuesta = rsp;
                }

                //if (resultadoDto.IsSuccess)
                //{
                //    var balanceDto = resultadoDto.Result;

                //    /*ACTUALIZAR BALANCE CON EL DTO*/
                //    balanceDto.EstadoContable = _mapper.Map<EstadoContable>(modelo);

                //    if (balanceDto.EstadoContable.otrosRubros == null)
                //        balanceDto.EstadoContable.otrosRubros = new List<RubroPatrimonioNeto>();

                //    foreach (var rubro in balanceDto.EstadoContable.otrosRubros.ToList())
                //    {
                //        rubro.Codigo = Guid.NewGuid().ToString();
                //        balanceDto.EstadoContable.otrosRubros.Add(rubro);
                //    }

                //    var rsp = _balanceBusiness.Update(balanceDto);

                //    respuesta = rsp;
                //}
            }
            catch (Exception ex)
            {
                respuesta.Message = ex.Message;
            }

            return respuesta;

        }




        private EstadoContable MappToContador(EstadoContableDto modelo)
        {

            var estadoContable = new EstadoContable()
            {
                FechaAsamblea = modelo.fechaAsamblea,
                CapitalSuscripto = modelo.capitalSuscripto,
                ActivoCorriente = modelo.activoCorriente,
                ActivoCorrienteRestante = modelo.activoCorrienteRestante,
                ActivoNoCorriente = modelo.activoNoCorriente,
                ActivoNoCorrienteRestante = modelo.activoNoCorrienteRestante,
                AjusteCapital = modelo.ajusteCapital,
                AportesIrrevocables = modelo.aportesIrrevocables,
                InversionesActivoCorriente = modelo.inversionesActivoCorriente,
                DeudorPasivoCorriente = modelo.deudorPasivoNoCorriente,
                BienesDeCambio = modelo.bienesDeCambio,
                BienesDeUso = modelo.bienesDeUso,
                InversionesActivoNoCorriente = modelo.inversionesActivoNoCorriente,
                PerdidasAcumuladas = modelo.perdidasAcumuladas,
                TotalActivo = modelo.totalActivo,
                CajaYBancos = modelo.cajaYBancos,
                DeudorPasivoNoCorriente = modelo.deudorPasivoNoCorriente,
                FechaEstado = modelo.fechaEstado,
                FechaInicio = modelo.fechaInicio,
                FechaReunionDirectorio = modelo.fechaReunionDirectorio,
                GananciasPerdidasEjercicio = modelo.gananciasPerdidasEjercicio,
                GananciasReservadas = modelo.gananciasReservadas,
                PasivoCorriente = modelo.pasivoCorriente,
                PasivoNoCorriente = modelo.pasivoNoCorriente,
                PatrimonioNeto = modelo.patrimonioNeto,
                ReservaLegal = modelo.reservaLegal,
                PropiedadesDeInversion = modelo.propiedadesDeInversion,
                PrimaEmision = modelo.primaEmision,
                TipoBalance = modelo.tipoBalance,
                TotalPasivo = modelo.totalPasivo,


            };

            return estadoContable;
        }
    }
}
