using AutoMapper;
using Balances.Bussiness.Contrato;
using Balances.DTO;
using Balances.Model;
using Balances.Repository.Contract;
using Balances.Services.Contract;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Balances.Bussiness
{
    public class BalanceBusiness : IBalanceBusiness
    {

        protected IMongoCollection<Balance> _balances;
        protected IMapper _mapper;
        private ISessionService _sessionService;

        public BalanceDto BalanceActual
        {
            get
            {
                var responseDto = this.GetById(_sessionService.BalanceId);

                if (responseDto.IsSuccess)
                {
                    return responseDto.Result;
                }
                else
                    return null;
            }
        }

        public BalanceBusiness(IMongoDbSettings _settings, IMapper mapper, ISessionService sessionService)
        {
            var cliente = new MongoClient(_settings.Server);
            var database = cliente.GetDatabase(_settings.Database);
            _balances = database.GetCollection<Balance>(_settings.Collection);
            _mapper = mapper;
            _sessionService = sessionService;

        }

        public ResponseDTO<bool> Delete(string id)
        {

            ResponseDTO<bool> respuesta = new ResponseDTO<bool>();
            respuesta.IsSuccess = false;

            try
            {


                var delete = _balances.DeleteOneAsync(d => d.Id == id);
                if (delete != null)
                {

                    respuesta.IsSuccess = true;
                    respuesta.Message = "balance eliminado correctamente";
                    respuesta.Result = true;
                }
            }
            catch (Exception ex)
            {
                respuesta.Message = ex.Message;
            }

            return respuesta;
        }

        public ResponseDTO<BalanceDto> GetById(string id)
        {
            ResponseDTO<BalanceDto> respuesta = new ResponseDTO<BalanceDto>();
            respuesta.IsSuccess = false;

            try
            {
                var balance = _balances.Find(
             new BsonDocument { { "_id", new ObjectId(id) } }
            ).FirstOrDefaultAsync().Result;

                var balancedto = _mapper.Map<BalanceDto>(balance);

                respuesta.Result = balancedto;
                respuesta.IsSuccess = true;
                respuesta.Message = "balance encontrado exitosamente";


            }
            catch (Exception ex)
            {
                //conoces el parametro


                /*_LogService.Error("GetbyId", ex,);
                */
                respuesta.Message = ex.Message;

            }
            return respuesta;


        }

        public ResponseDTO<IEnumerable<BalanceDto>> List()

        {
            ResponseDTO<IEnumerable<BalanceDto>> respuesta = new ResponseDTO<IEnumerable<BalanceDto>>();
            respuesta.IsSuccess = false;
            try
            {
                var listaBalance = _balances.Find(p => true).ToList();
                var listabalanceDto = _mapper.Map<List<BalanceDto>>(listaBalance);

                respuesta.IsSuccess = true;
                respuesta.Result = listabalanceDto;
                respuesta.Message = "Listado de balances cargados correctamente";


            }
            catch (Exception ex)
            {
                respuesta.Message = ex.Message;

            }
            return respuesta;
        }

        public ResponseDTO<BalanceDto> Insert(BalanceDto modelo)
        {

            ResponseDTO<BalanceDto> respuesta = new ResponseDTO<BalanceDto>();
            respuesta.IsSuccess = false;

            try
            {
                var balance = _mapper.Map<Balance>(modelo);
                var rsp = _balances.InsertOneAsync(balance);

                // si inserto correctamente
                if (rsp != null)
                {
                    var balancedto = _mapper.Map<BalanceDto>(balance);
                    //balance.Id = rsp.Id.ToString();
                    respuesta.IsSuccess = true;
                    respuesta.Result = balancedto;

                }

                return respuesta;
            }
            catch (Exception ex)
            {

                respuesta.Message = ex.Message;
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
                _balances.ReplaceOneAsync(b => b.Id == balance.Id, balance);
                var balancedto = _mapper.Map<BalanceDto>(balance);

                respuesta.IsSuccess = true;
                respuesta.Result = balancedto;
                respuesta.Message = "balance actualizado correctamente";
            }
            catch (Exception ex)
            {
                respuesta.Message = ex.Message;
            }

            return respuesta;
        }
    }




}
