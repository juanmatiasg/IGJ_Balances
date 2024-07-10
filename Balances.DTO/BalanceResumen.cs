using Balances.Model;
using System.Security.Cryptography;
using System.Text;

namespace Balances.DTO
{
    public class BalanceResumen
    {
        public string Id { get; set; }
        public Caratula Caratula { get; set; }
        public List<AutoridadDto> Autoridades { get; set; }
        public EstadoContable EstadoContable { get; set; }
        public LibrosDto Libros { get; set; }
        public Contador Contador { get; set; }
        public SociosDto Socios { get; set; }

    }
    public static class BalanceMapper
    {
        public static BalanceResumen MapToResumen(BalanceDto balance)
        {
            return new BalanceResumen
            {
                Id = balance.Id,
                Caratula = balance.Caratula,
                Autoridades = balance.Autoridades,
                EstadoContable = balance.EstadoContable,
                Libros = balance.Libros,
                Socios = balance.Socios,
                Contador = balance.Contador
            };
        }
    }

    public static class HashHelper
    {
        public static string CalculateHash(string balanceResumen)
        {
            using (SHA256 sha256 = SHA256.Create())
            {

                //string rawData = $"{balanceResumen.Id}|{balanceResumen.Caratula}|{balanceResumen.EstadoContable}|{balanceResumen.Libros}|{balanceResumen.Socios}|{balanceResumen.Contador}";
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(balanceResumen));

                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }

}
