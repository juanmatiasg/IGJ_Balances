using static Balances.DTO.LibrosDto;

namespace Balances.DTO
{
    public class LibroDtoExtended
    {
        public LibroDto Original { get; set; } = new LibroDto();
        public string Tipo { get { return Original.Tipo; } set { Original.Tipo = value; } }
        public string Nombre { get { return Original.Nombre; } set { Original.Nombre = value; } }
       
        // public string Denominacion { get { return Original.Denominacion; } set { Original.Denominacion = value; } }
        public string NumeroRubrica { get { return Original.NumeroRubrica; } set { Original.NumeroRubrica = value; } }
       
        public DateTime FechaUltimaRegistracion { get { return Original.FechaUltimaRegistracion; } set { Original.FechaUltimaRegistracion = value; } }
        public DateTime FechaRubrica { get { return Original.FechaRubrica; } set { Original.FechaRubrica = value; } }
        
        public string Folio { get { return Original.Folio; } set { Original.Folio = value; } }


        public bool NoSabeNoContesta
        {
            get { return Original.NoSabeNoContesta; }

            set
            {
                if (value)
                    setNC();
                else
                    clear();
                Original.NoSabeNoContesta = value;
            }
        }
        public LibroDtoExtended OldValue { get; set; }

        public void setNC()
        {
            this.OldValue = new LibroDtoExtended(this);
            this.Nombre = "N/C";
           // this.Denominacion = "N/C";
            this.NumeroRubrica = "N/C";
            this.FechaRubrica = DateTime.MinValue;
            this.FechaUltimaRegistracion = DateTime.MinValue;
            this.Folio = "N/C";
            

        }
        public void clear()
        {
            if (OldValue != null)
            {
                this.Nombre = OldValue.Nombre;
                //this.Denominacion = OldValue.Denominacion;
                this.NumeroRubrica = OldValue.NumeroRubrica;
                this.FechaRubrica = OldValue.FechaRubrica;
                this.FechaUltimaRegistracion = OldValue.FechaUltimaRegistracion;
                this.Folio = OldValue.Folio;
            }
        }

        public LibroDtoExtended(LibroDtoExtended other)
        {
            this.Nombre = other.Nombre;
            //this.Denominacion = other.Denominacion;
            this.NumeroRubrica = other.NumeroRubrica;
            this.FechaRubrica = other.FechaRubrica;
            this.FechaUltimaRegistracion = other.FechaUltimaRegistracion;
            this.Folio = other.Folio;
        }
        public LibroDtoExtended() { }
    }
    

    

    
}
