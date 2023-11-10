using Balances.Model;

namespace Balances.DTO
{
    public class ContadorDto
    {
        public ContadorDto() { }

        public string nombre { get; set; }
        public string apellido { get; set; }
        public string tipoDocumento { get; set; }
        public string nroDocumento { get; set; }
        public string nroFiscal { get; set; }
        public string tomo { get; set; }
        public string folio { get; set; }
        public DateTime fechaInformeAuditorExt { get; set; }
        public string nroLegalInfoAudExt { get; set; }


        public ContadorDto(Contador contador)
        {


            nombre = contador.ContadorNombre;
            apellido = contador.ContadorApellido;
            tipoDocumento = contador.TipoDocumento;
            nroDocumento = contador.ContadorNroDocumento;
            nroFiscal = contador.ContadorNroFiscal;
            tomo = contador.Tomo;
            folio = contador.Folio;
            fechaInformeAuditorExt = contador.FechaInformeAuditorExt;
            nroLegalInfoAudExt = contador.NroLegalInfoAudExt;



        }



        public Contador GetContador()
        {
            var contador = new Contador
            {
                TipoDocumento = tipoDocumento,
                ContadorNombre = nombre,
                ContadorApellido = apellido,
                ContadorNroDocumento = nroDocumento,
                ContadorNroFiscal = nroFiscal,
                Tomo = tomo,
                Folio = folio,
                FechaInformeAuditorExt = fechaInformeAuditorExt,
                NroLegalInfoAudExt = nroLegalInfoAudExt
            };

            return contador;


        }
    }
}
