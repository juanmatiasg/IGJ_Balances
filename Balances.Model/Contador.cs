﻿namespace Balances.Model
{
    public class Contador
    {
        

        public string Nombre { get; set; }
        public string Apellido { get; set; }

        public string NroDocumento { get; set; }

        public string TipoDocumento{ get; set; }
        public string NroFiscal { get; set; }
        public string Tomo { get; set; }
        public string Folio { get; set; }
        public DateTime FechaInformeAuditorExt { get; set; }
        public string NroLegalInfoAudExt { get; set; }

        //public static implicit operator Contador(Balances.DTO.ContadorDto v)
        //{
        //    throw new NotImplementedException();
        //}
        //public string Estado { get; internal set; }
    }
}
