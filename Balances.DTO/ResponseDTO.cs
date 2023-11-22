namespace Balances.DTO
{
    public class ResponseDTO<T>
    {
        public T? Result { get; set; }

        public bool IsSuccess { get; set; }

        public string? Message { get; set; }

    }
}
