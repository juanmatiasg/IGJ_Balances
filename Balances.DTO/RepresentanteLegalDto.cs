using Balances.Model;

namespace Balances.DTO
{
    public class RepresentanteLegalDto
    {
        public RepresentanteLegalDto() { }
        public RepresentanteLegalDto(RepresentanteLegal representanteLegal)
        {
            id = representanteLegal.Id;
            string nombreCompleto = representanteLegal.RepresentanteApellido == null ? null : $"{representanteLegal.RepresentanteApellido.Trim()}, {representanteLegal.RepresentanteNombre}";


            representanteNombre = representanteLegal.RepresentanteNombre;
            representanteApellido = representanteLegal.RepresentanteApellido;

            representanteNombreCompleto = nombreCompleto;
            representanteTipoDocumento = representanteLegal.RepresentanteTipoDocumento;
            representanteNroDocumento = representanteLegal.RepresentanteNroDocumento;
            representanteNroFiscal = representanteLegal.RepresentanteNroFiscal;
            representanteCargo = representanteLegal.RepresentanteCargo;
            esFirmante = representanteLegal.EsFirmante;

        }

        public RepresentanteLegal GetRepresentanteLegal()
        {
            return new RepresentanteLegal
            {
                RepresentanteApellido = representanteApellido,
                RepresentanteNombre = representanteNombre,
                RepresentanteTipoDocumento = representanteTipoDocumento,
                RepresentanteNroDocumento = representanteNroDocumento,
                RepresentanteNroFiscal = representanteNroFiscal,
                RepresentanteCargo = representanteCargo,
                EsFirmante = (bool)esFirmante


            };
        }





        public string id { get; set; }
        public string representanteNombre { get; set; }
        public string representanteApellido { get; set; }

        public string representanteNombreCompleto { get; set; }
        public string representanteTipoDocumento { get; set; }
        public string representanteNroDocumento { get; set; }
        public string representanteNroFiscal { get; set; }
        public string representanteCargo { get; set; }

        public bool esFirmante { get; set; }
    }
}
