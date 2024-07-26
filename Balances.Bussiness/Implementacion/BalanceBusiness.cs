using AutoMapper;
using Balances.Bussiness.Contrato;
using Balances.DTO;
using Balances.Model;
using Balances.Repository.Contract;
using Balances.Services.Contract;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace Balances.Bussiness
{
    public class BalanceBusiness : IBalanceBusiness
    {

        protected IMongoCollection<Balance> _balances;
        protected IMapper _mapper;
        private ISessionService _sessionService;
        private readonly ILogger<BalanceBusiness> _logger;
        private readonly IBalanceService _balanceService;
        public BalanceDto BalanceActual { get; set; }

        public BalanceBusiness(IMongoDbSettings _settings,
                               IMapper mapper,
                               ISessionService sessionService,
                               ILogger<BalanceBusiness> logger,
                               IBalanceService balanceService)
        {
            var cliente = new MongoClient(_settings.Server);
            var database = cliente.GetDatabase(_settings.Database);
            _balances = database.GetCollection<Balance>(_settings.Collection);
            _mapper = mapper;
            _sessionService = sessionService;
            _logger = logger;
            _balanceService = balanceService;
        }

        /*public BalanceDto BalanceActual
        {
            get
            {
                
                var responseDto = this.GetById(_sessionService.GetSessionBalanceId());
                var responseDto = this.GetById(_sessionService.GetBalanceId());

                if (responseDto.IsSuccess)
                {
                    _logger.LogInformation("BalanceBusiness.BalanceActual GetById : exitoso");
                    return responseDto.Result;

                }
                else

                    _logger.LogError($"BalanceBusiness.BalanceActual: null");
                return null;
            }
        }*/

        public ResponseDTO<bool> Delete(string id)
        {

            ResponseDTO<bool> respuesta = new ResponseDTO<bool>();
            respuesta.IsSuccess = false;

            try
            {

                var delete = _balanceService.DeleteBalance(id);
                //var delete = _balances.DeleteOneAsync(d => d.Id == id);
                if (delete)
                {

                    respuesta.IsSuccess = true;
                    respuesta.Message = "balance eliminado correctamente";
                    respuesta.Result = true;
                    _logger.LogInformation("BalanceBusiness.Delete: exitoso");
                }
            }
            catch (Exception ex)
            {
                respuesta.Message = ex.Message;
                _logger.LogError($"BalanceBusiness.Delete: \n {ex}");
            }

            return respuesta;
        }

        public ResponseDTO<BalanceDto> GetById(string id)
        {
            ResponseDTO<BalanceDto> respuesta = new ResponseDTO<BalanceDto>();
            respuesta.IsSuccess = false;

            try
            {
                var balance = _balanceService.GetById(id);

                var balancedto = _mapper.Map<BalanceDto>(balance);

                respuesta.Result = balancedto;
                this.BalanceActual = balancedto;
                respuesta.IsSuccess = true;
                respuesta.Message = "balance encontrado exitosamente";
                _logger.LogInformation("BalanceBusiness.GetById: correctamente");

            }
            catch (Exception ex)
            {

                respuesta.Message = ex.Message;
                _logger.LogError($"BalanceBusiness.GetById: \n {ex}");

            }
            return respuesta;


        }

        public ResponseDTO<IEnumerable<BalanceDto>> List(string correlativo)

        {
            ResponseDTO<IEnumerable<BalanceDto>> respuesta = new ResponseDTO<IEnumerable<BalanceDto>>();
            respuesta.IsSuccess = false;
            try
            {
                var listaBalance = _balanceService.GetAll(correlativo);
                //var listaBalance = _balances.Find(p => true).ToList();
                var listabalanceDto = _mapper.Map<List<BalanceDto>>(listaBalance);

                respuesta.IsSuccess = true;
                respuesta.Result = listabalanceDto;
                respuesta.Message = "Listado de balances cargados correctamente";
                _logger.LogInformation("BalanceBusiness.List: correctamente");


            }
            catch (Exception ex)
            {
                respuesta.Message = ex.Message;
                _logger.LogError($"BalanceBusiness.List: \n {ex}");
            }
            return respuesta;
        }

        public ResponseDTO<Balance> Insert(Balance modelo)
        {

            ResponseDTO<Balance> respuesta = new ResponseDTO<Balance>();
            respuesta.IsSuccess = false;

            try
            {
                var balance = _mapper.Map<Balance>(modelo);

                var rsp = _balanceService.InsertBalance(balance);

                // si inserto correctamente
                if (rsp)
                {
                    var balancedto = _mapper.Map<BalanceDto>(balance);
                    respuesta.IsSuccess = true;
                    respuesta.Result = balance;


                    _logger.LogInformation("BalanceBusiness.Insert: correctamente");
                }

                return respuesta;
            }
            catch (Exception ex)
            {

                respuesta.Message = ex.Message;
                _logger.LogError($"BalanceBusiness.Insert: \n {ex}");
                return respuesta;
            }




        }

        public ResponseDTO<BalanceDto> Update(BalanceDto modelo)
        {
            ResponseDTO<BalanceDto> respuesta = new ResponseDTO<BalanceDto>();
            respuesta.IsSuccess = false;

            try
            {
                var balance = _mapper.Map<Balance>(modelo);
                _balanceService.UpdateBalance(balance);
                //_balances.ReplaceOneAsync(b => b.Id == balance.Id, balance);
                var balancedto = _mapper.Map<BalanceDto>(balance);

                respuesta.IsSuccess = true;
                respuesta.Result = balancedto;
                respuesta.Message = "balance actualizado correctamente";
                _logger.LogInformation("BalanceBusiness.Update: correctamente");
            }
            catch (Exception ex)
            {
                respuesta.Message = ex.Message;
                _logger.LogError($"BalanceBusiness.Update: \n {ex}");
            }

            return respuesta;
        }


    }


}
