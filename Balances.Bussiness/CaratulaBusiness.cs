using AutoMapper;
using Balances.Bussiness.Contrato;
using Balances.DTO;
using Balances.Model;
using Balances.Repository.Contract;
using Balances.Services.Contract;
using MongoDB.Driver;

namespace Balances.Bussiness
{
    public class CaratulaBusiness : ICaratulaBusiness
    {

        protected IMongoCollection<Balance> _balance;
        protected IMapper _mapper;
        private ISessionService _sessionService;
        private readonly IBalanceBusiness _balanceBusiness;


        public CaratulaBusiness(IMongoDbSettings _settings, IMapper mapper, ISessionService sessionService, IBalanceBusiness balanceBusiness)
        {
            var cliente = new MongoClient(_settings.Server);
            var database = cliente.GetDatabase(_settings.Database);
            _balance = database.GetCollection<Balance>(_settings.Collection);
            _mapper = mapper;
            _sessionService = sessionService;
            _balanceBusiness = balanceBusiness;
        }

        public bool Delete(CaratulaDto modelo)
        {
            throw new NotImplementedException();
        }

        //public ResponseDTO<CaratulaDto> GetById(string id)
        //{
        //    var respuesta = new ResponseDTO<CaratulaDto>();
        //    respuesta.IsSuccess = false;

        //    try
        //    {
        //        var responseDTO = _balanceBusiness.GetById(id);

        //        if (responseDTO.IsSuccess)
        //        {
        //            var balance = responseDTO.Result;
        //            balance.Caratula = _mapper.Map<Caratula>(caratuladto);
        //        }
        //        var caratuladto = balancersp.Result.Caratula;
        //        var caratula = 


        //        respuesta.IsSuccess = true;
        //        respuesta.Result = caratula;
        //        respuesta.Message = "caratula encontrada exitosamente";
        //    }
        //    catch (Exception ex)
        //    {

        //        respuesta.Message = ex.Message;

        //    }

        //    return respuesta;
        //}

        public ResponseDTO<Balance> Insert(CaratulaDto modelo)
        {
            ResponseDTO<Balance> respuesta = new ResponseDTO<Balance>();
            respuesta.IsSuccess = false;

            try
            {
                var balance = new Balance();


                balance.Caratula = MapearCaratula(modelo);

                var rsp = _balance.InsertOneAsync(balance);


                // si inserto correctamente
                if (rsp != null)
                {
                    //_sessionService.SetBalanceId(balance.Id);
                    //balance.Id = rsp.Id.ToString();
                    respuesta.IsSuccess = true;
                    respuesta.Message = "Caratula creada correctamente";
                    respuesta.Result = balance;

                }
                else
                    respuesta.Message = "No se Pudo Insertar";

            }
            catch (Exception ex)
            {
                respuesta.Message = ex.Message;


            }
            return respuesta;
        }

        private Caratula MapearCaratula(CaratulaDto modelo)
        {
            var caratula = new Caratula()
            {
                FechaDeCierre = modelo.FechaDeCierre,
                Email = modelo.Email,
                Entidad = modelo.Entidad,
                FechaInicio = modelo.FechaInicio,
            };

            return caratula;
        }

        private CaratulaDto MapearCaratulaDto(Caratula modelo)
        {
            var caratuladto = new CaratulaDto()
            {
                FechaDeCierre = modelo.FechaDeCierre,
                Email = modelo.Email,
                Entidad = modelo.Entidad,
                FechaInicio = modelo.FechaInicio,
            };

            return caratuladto;
        }

        public IEnumerable<CaratulaDto> List()
        {
            throw new NotImplementedException();
        }

        public bool Update(CaratulaDto modelo)
        {
            throw new NotImplementedException();
        }







        //public CaratulaDto Insert(CaratulaDto modelo)
        //{
        //    try
        //    {
        //        var balance = _mapper.Map<Balance>(modelo);
        //        var rsp = _balances.InsertOneAsync(balance);

        //        if (rsp.Id != null) return _mapper.Map<BalanceDto>(rsp);
        //        else throw new NotImplementedException("No se pudo crear");
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}


    }
}
