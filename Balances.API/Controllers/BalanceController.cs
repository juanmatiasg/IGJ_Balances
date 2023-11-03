using Balances.DTO;
using Balances.Model;
using Balances.Services.Contract;
using Microsoft.AspNetCore.Mvc;

namespace Balances.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BalanceController : ControllerBase
    {
        private readonly IBalanceService _balanceService;

        public BalanceController(IBalanceService balanceService)
        {
            _balanceService = balanceService;
        }

      
        [HttpPost]
        public IActionResult Create(BalanceRequestDTO balanceRequest)
        {
            var createdBalance =  _balanceService.Create(balanceRequest);
            var response = new ResponseDTO<BalanceResponseDTO>
            {
                Result = createdBalance, 
                IsSuccess = true,
                Message = "Balance created successfully"
            };
            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            var balance = _balanceService.GetById(id);
            if (balance == null)
            {
                return NotFound();
            }
            var response = new ResponseDTO<BalanceResponseDTO>
            {
                Result = balance, 
                IsSuccess = true,
                Message = "Balance found"
            };
            return Ok(response);
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            var balances =  _balanceService.GetAll();
            var response = new ResponseDTO<List<BalanceResponseDTO>>
            {
                Result = balances,
                IsSuccess = true,
                Message = "Balances retrieved successfully"
            };
            return Ok(response);
        }
    

        [HttpPut]
        public IActionResult Update(BalanceRequestDTO balanceRequest)
        {
            var isUpdated =  _balanceService.Update(balanceRequest);
            var response = new ResponseDTO<bool>
            {
                Result = isUpdated,
                IsSuccess = isUpdated,
                Message = isUpdated ? "Balance updated successfully" : "Update failed"
            };

            if (!isUpdated)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var isDeleted =  _balanceService.Delete(id);
            var response = new ResponseDTO<bool>
            {
                Result = isDeleted,
                IsSuccess = isDeleted,
                Message = isDeleted ? "Balance deleted successfully" : "Deletion failed"
            };

            if (!isDeleted)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}
