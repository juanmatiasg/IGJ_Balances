namespace Balances.DTO
{
    public class LibroDtoExtended
    {
        public LibroDto Original { get; set; } = new LibroDto();
        public string Tipo { get => Original.Tipo; set => Original.Tipo = value; }
        public string Nombre { get => Original.Nombre; set => Original.Nombre = value; }
        public string NumeroRubrica { get => Original.NumeroRubrica; set => Original.NumeroRubrica = value; }
        public DateTime? FechaUltimaRegistracion { get => Original.FechaUltimaRegistracion ?? null; set => Original.FechaUltimaRegistracion = value; }
        public DateTime? FechaRubrica { get => Original.FechaRubrica ?? null; set => Original.FechaRubrica = value; }
        public string FolioObraTranscripcion { get => Original.FolioObraTranscripcion; set => Original.FolioObraTranscripcion = value; }
        public string FolioUltimaRegistracion { get => Original.FolioUltimaRegistracion; set => Original.FolioUltimaRegistracion = value; }
        public bool NoSabeNoContesta
        {
            get => Original.NoSabeNoContesta;
            set
            {
                if (value)
                    SetNC();
                else
                    Clear();

                Original.NoSabeNoContesta = value;
            }
        }

        public LibroDtoExtended OldValue { get; set; }

        public void SetNC()
        {
            OldValue = new LibroDtoExtended(this);
            Nombre = "N/C";
            NumeroRubrica = "N/C";
            FechaRubrica = null;
            FechaUltimaRegistracion = null;
            FolioObraTranscripcion = "N/C";
            FolioUltimaRegistracion = "N/C";
        }

        public void Clear()
        {
            if (OldValue != null)
            {
                Nombre = OldValue.Nombre;
                NumeroRubrica = OldValue.NumeroRubrica;
                FechaRubrica = OldValue.FechaRubrica;
                FechaUltimaRegistracion = OldValue.FechaUltimaRegistracion;
                FolioObraTranscripcion = OldValue.FolioObraTranscripcion;
                FolioUltimaRegistracion = OldValue.FolioUltimaRegistracion;

            }
            else
            {
                Nombre = "";
                NumeroRubrica = "";
                FechaRubrica = null;
                FechaUltimaRegistracion = null;
                FolioObraTranscripcion = "";
                FolioUltimaRegistracion = "";
            }

        }

        public LibroDtoExtended(LibroDtoExtended other)
        {
            Tipo = other.Tipo;
            Nombre = other.Nombre;
            NumeroRubrica = other.NumeroRubrica;
            FechaRubrica = other.FechaRubrica;
            FechaUltimaRegistracion = other.FechaUltimaRegistracion;
            FolioObraTranscripcion = other.FolioObraTranscripcion;
            FolioUltimaRegistracion = other.FolioUltimaRegistracion;
        }

        public LibroDtoExtended() { }
    }
}
