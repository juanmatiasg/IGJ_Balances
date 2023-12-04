using AutoMapper;
using Balances.Bussiness.Contrato;
using Balances.DTO;
using Balances.Model;
using Balances.Services.Contract;

namespace Balances.Bussiness.Implementacion
{
    public class ContadorBusiness : IContadorBusiness

    {

        //protected IMongoCollection<Balance> _balance;
        private readonly ISessionService _sessionService;
        private readonly IBalanceBusiness _balanceBusiness;
        private readonly IMapper _mapper;


        public ContadorBusiness(/*IMongoDbSettings _settings*/ ISessionService sessionService,
                               IBalanceBusiness balanceBusiness, IMapper mapper)
        {
            _sessionService = sessionService;
            _balanceBusiness = balanceBusiness;
            _mapper = mapper;
            //var cliente = new MongoClient(_settings.Server);
            //var database = cliente.GetDatabase(_settings.Database);
            //_balance = database.GetCollection<Balance>(_settings.Collection);
        }



        public ResponseDTO<BalanceDto> Delete(ContadorDto modelo)
        {
            ResponseDTO<BalanceDto> respuesta = new ResponseDTO<BalanceDto>();
            respuesta.IsSuccess = false;

            try
            {
                //BUSCO BALANCE



            }
            catch (Exception)
            {

                throw;
            }




            throw new NotImplementedException();
        }

        public ResponseDTO<BalanceDto> Insert(ContadorDto modelo)
        {
            ResponseDTO<BalanceDto> respuesta = new ResponseDTO<BalanceDto>();
            respuesta.IsSuccess = false;

            try
            {
                var id = _sessionService.GetBalanceId();
                var responsedto = _balanceBusiness.GetById(modelo.id);

                if (responsedto.IsSuccess)
                {
                    var balance = responsedto.Result;
                    //mapeamos el contador al balance 

                    balance.Contador = MappToContador(modelo);

                    var rsp = _balanceBusiness.Update(balance);

                    // si inserto correctamente
                    if (rsp.IsSuccess)
                    {
                        //_sessionService.SetBalanceId(balancedto.Result.Id);

                        respuesta.IsSuccess = true;
                        respuesta.Message = "Contador generado correctamente";

                        respuesta.Result = balance;

                    }

                }
                else
                    respuesta.Message = "No se encontro el balance";
            }
            catch (Exception ex)
            {
                respuesta.Message = ex.Message;
            }

            return respuesta;
        }

        public IEnumerable<ContadorDto> List()
        {
            throw new NotImplementedException();
        }

        public bool Update(ContadorDto modelo)
        {
            throw new NotImplementedException();
        }

        private ContadorDto MappToContadorDto(Contador modelo)
        {
            var contadordto = new ContadorDto()
            {
                NroDocumento = modelo.NroDocumento,
                Apellido = modelo.Nombre,
                FechaInformeAuditorExt = modelo.FechaInformeAuditorExt,
                Folio = modelo.Folio,
                Nombre = modelo.Nombre,
                NroFiscal = modelo.NroFiscal,
                TipoDocumento = modelo.TipoDocumento,
                NroLegalInfoAudExt = modelo.NroLegalInfoAudExt,
                Tomo = modelo.Tomo,
            };

            return contadordto;
        }

        public ContadorDto GetById(string id)
        {


            try
            {
                var balance = _balanceBusiness.GetById(id);

                var contadordto = MappToContadorDto(balance.Result.Contador);

                return contadordto;





            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        private Contador MappToContador(ContadorDto modelo)
        {
            var contador = new Contador()
            {
                Apellido = modelo.Apellido,
                Nombre = modelo.Nombre,
                NroDocumento = modelo.NroDocumento,
                NroFiscal = modelo.NroFiscal,
                FechaInformeAuditorExt = modelo.FechaInformeAuditorExt,
                Folio = modelo.Folio,
                NroLegalInfoAudExt = modelo.NroLegalInfoAudExt,
                TipoDocumento = modelo.TipoDocumento,
                Tomo = modelo.Tomo,
            };

            return contador;
        }





    }
}
