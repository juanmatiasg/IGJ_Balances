namespace Balances.DTO
{
    public class BalanceRequestDTO
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public EntidadRequestDTO Entidad { get; set; }
    }
}
