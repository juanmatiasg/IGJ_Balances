namespace Balances.DTO
{
    public class LibrosDto
    {

        public LibrosDto()
        {
            this.Administracion = new LibroDto();
            this.Asamblea = new LibroDto();
            this.AsistenciaAsamblea = new LibroDto();
            this.Fiscalizacion = new LibroDto();
            this.Efectivo = new LibroDto();
            this.EstadosContablesConsolidados = new LibroDto();
            this.Auditor = new LibroDto();
            this.SituacionPatrimonial = new LibroDto();
            this.Informacion = new LibroDto();
            this.IVA = new LibroDto();
            this.IVACompras = new LibroDto();
            this.IVAVentas = new LibroDto();
            this.PatrimonioNeto = new LibroDto();
            this.Memoria = new LibroDto();
            this.Resultados = new LibroDto();
            this.LibroDiario = new LibroDto();

        }

        public string SessionId { get; set; }

        private LibroDto _Memoria;
        public LibroDto Memoria
        {
            get { return _Memoria; }
            set
            {
                if (value == null) value = new LibroDto();

                value.Tipo = "Memoria";
                _Memoria = value;
            }
        }
        private LibroDto _Administracion;

        public LibroDto Administracion
        {
            get { return _Administracion; }
            set { if (value == null) value = new LibroDto(); value.Tipo = "Acta del Organo de Administración"; _Administracion = value; }
        }

        private LibroDto _Asamblea;
        public LibroDto Asamblea
        {
            get { return _Asamblea; }
            set { if (value == null) value = new LibroDto(); value.Tipo = "Acta de Reunión Socios"; _Asamblea = value; }

        }

        private LibroDto _AsistenciaAsamblea;
        public LibroDto AsistenciaAsamblea
        {
            get { return _AsistenciaAsamblea; }
            set { if (value == null) value = new LibroDto(); value.Tipo = "Registro de Asistencia"; _AsistenciaAsamblea = value; }

        }

        private LibroDto _SituacionPatrimonial;
        public LibroDto SituacionPatrimonial
        {
            get { return _SituacionPatrimonial; }
            set { if (value == null) value = new LibroDto(); value.Tipo = "Estado de Situación Patrimonial"; _SituacionPatrimonial = value; }

        }

        private LibroDto _Resultados;


        public LibroDto Resultados
        {
            get { return _Resultados; }
            set { if (value == null) value = new LibroDto(); value.Tipo = "Estado de Resultados"; _Resultados = value; }
        }

        private LibroDto _PatrimonioNeto;
        public LibroDto PatrimonioNeto
        {
            get { return _PatrimonioNeto; }
            set { if (value == null) value = new LibroDto(); value.Tipo = "Estado de Evolución del Patrimonio Neto"; _PatrimonioNeto = value; }
        }

        private LibroDto _Efectivo;
        public LibroDto Efectivo
        {
            get { return _Efectivo; }
            set { if (value == null) value = new LibroDto(); value.Tipo = "Estado de Flujo de Efectivo"; _Efectivo = value; }
        }

        private LibroDto _Informacion;
        public LibroDto Informacion
        {
            get { return _Informacion; }
            set { if (value == null) value = new LibroDto(); value.Tipo = "Informacion Complementaria"; _Informacion = value; }

        }

        private LibroDto _EstadosContablesConsolidados;
        public LibroDto EstadosContablesConsolidados
        {
            get { return _EstadosContablesConsolidados; }
            set { if (value == null) value = new LibroDto(); value.Tipo = "Estados Contables Consolidados"; _EstadosContablesConsolidados = value; }
        }

        private LibroDto _Fiscalizacion;
        public LibroDto Fiscalizacion
        {
            get { return _Fiscalizacion; }
            set { if (value == null) value = new LibroDto(); value.Tipo = "Informe Organo Fiscalización"; _Fiscalizacion = value; }
        }

        private LibroDto _Auditor;
        public LibroDto Auditor
        {
            get { return _Auditor; }
            set { if (value == null) value = new LibroDto(); value.Tipo = "Informe Auditor Externo"; _Auditor = value; }
        }

        private LibroDto _IVA;
        public LibroDto IVA
        {
            get { return _IVA; }
            set { if (value == null) value = new LibroDto(); value.Tipo = "IVA"; _IVA = value; }
        }

        private LibroDto _IVACompras;
        public LibroDto IVACompras
        {
            get { return _IVACompras; }
            set { if (value == null) value = new LibroDto(); value.Tipo = "IVA Compras"; _IVACompras = value; }
        }

        private LibroDto _IVAVentas;
        public LibroDto IVAVentas
        {
            get { return _IVAVentas; }
            set { if (value == null) value = new LibroDto(); value.Tipo = "IVA Ventas"; _IVAVentas = value; }
        }

        private LibroDto _LibroDiario;
        public LibroDto LibroDiario
        {
            get { return _LibroDiario; }
            set { if (value == null) value = new LibroDto(); value.Tipo = "Libro Diario"; _LibroDiario = value; }
        }

    }
}

