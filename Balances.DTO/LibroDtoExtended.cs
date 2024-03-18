using System;

namespace Balances.DTO
{
    public class LibroDtoExtended
    {
        public LibroDto Original { get; set; } = new LibroDto();
        public string Tipo { get => Original.Tipo; set => Original.Tipo = value; }
        public string Nombre { get => Original.Nombre; set => Original.Nombre = value; }
        public string NumeroRubrica { get => Original.NumeroRubrica; set => Original.NumeroRubrica = value; }
        public DateTime? FechaUltimaRegistracion { get => Original.FechaUltimaRegistracion; set => Original.FechaUltimaRegistracion = value; }
        public DateTime? FechaRubrica { get => Original.FechaRubrica; set => Original.FechaRubrica = value; }
        public string Folio { get => Original.Folio; set => Original.Folio = value; }
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
            FechaRubrica = DateTime.Now;
            FechaUltimaRegistracion = DateTime.Now;
            Folio = "N/C";
        }

        public void Clear()
        {
            if (OldValue != null)
            {
                Nombre = OldValue.Nombre;
                NumeroRubrica = OldValue.NumeroRubrica;
                FechaRubrica = OldValue.FechaRubrica;
                FechaUltimaRegistracion = OldValue.FechaUltimaRegistracion;
                Folio = OldValue.Folio;
            }
            else {
                Nombre = "";
                NumeroRubrica = "";
                FechaRubrica = OldValue.FechaRubrica;
                FechaUltimaRegistracion = OldValue.FechaUltimaRegistracion;
                Folio = "";
            }
        }

        public LibroDtoExtended(LibroDtoExtended other)
        {
            Tipo = other.Tipo;
            Nombre = other.Nombre;
            NumeroRubrica = other.NumeroRubrica;
            FechaRubrica = other.FechaRubrica;
            FechaUltimaRegistracion = other.FechaUltimaRegistracion;
            Folio = other.Folio;
        }

        public LibroDtoExtended() { }
    }
}
