using Balances.Model;

namespace Balances.DTO
{
   public class RubrosPatrimonioNetoDto
    {
     
        public string? BalanceId { get; set; }
        public List<RubroPatrimonioNetoDto> otrosRubros { get; set; }

    }
}
