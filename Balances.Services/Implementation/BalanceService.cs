using AutoMapper;
using Balances.DTO;
using Balances.Model;
using Balances.Repository.Contract;
using Balances.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balances.Services.Implementation
{
    public class BalanceService : IBalanceService
    {
        private readonly IMongoRepository _modelRepository;
        private readonly IMapper _mapper;

        public BalanceService(IMongoRepository modelRepository, IMapper mapper)
        {
            _modelRepository = modelRepository;
            _mapper = mapper;
        }

        public BalanceResponseDTO Create(BalanceRequestDTO modelo)
        {
            var balance = _mapper.Map<Balance>(modelo);
             _modelRepository.CreateBalance(balance);
            return _mapper.Map<BalanceResponseDTO>(balance);
        }

        public bool Delete(string id)
        {
            var balance =  _modelRepository.GetBalance(id);
            if (balance == null)
                return false;

            _modelRepository.DeleteBalance(id);
            return true;
        }

        public BalanceResponseDTO GetById(string id)
        {
            var balance =  _modelRepository.GetBalance(id);
            return _mapper.Map<BalanceResponseDTO>(balance);
        }

        public  List<BalanceResponseDTO> GetAll()
        {
            var balances =  _modelRepository.GetAll(); 
            return _mapper.Map<List<BalanceResponseDTO>>(balances);
        }

        public  bool Update(BalanceRequestDTO modelo)
        {
            var balance =  _modelRepository.GetBalance(modelo.Id);
            if (balance == null)
                return false;

            var updatedBalance = _mapper.Map<Balance>(modelo);
             _modelRepository.UpdateBalance(modelo.Id, updatedBalance);
            return true;
        }
    }

}
