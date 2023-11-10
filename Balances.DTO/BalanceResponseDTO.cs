using Balances.Model;

namespace Balances.DTO
{
    public class BalanceResponseDTO
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public EntidadResponseDTO Entidad { get; set; }
        public Archivo Archivo { get; set; }
        public ICollection<Archivo> Archivos { get; set; }
        public RepresentanteLegales RepresentantesLegales { get; set; }
        public EstadoContable EstadoContable { get; set; }
        public List<LibroDigital> LibrosDigitales { get; set; }
        public Contador Contador { get; set; }
    }
}
