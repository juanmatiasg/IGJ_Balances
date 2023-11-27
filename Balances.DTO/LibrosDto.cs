﻿namespace Balances.DTO
{
    public class LibrosDto
    {
        //public string BalanceId { get; set; }
        //public List<LibroDto> librosDigitales { get; set; }
        public LibrosDto()
        {
            _Administracion = new LibroDto();
            _Asamblea = new LibroDto();
            _AsistenciaAsamblea = new LibroDto();
            _Auditor = new LibroDto();
            _Fiscalizacion = new LibroDto();
            _Efectivo = new LibroDto();
            _EstadosContablesConsolidados = new LibroDto();
            _Auditor = new LibroDto();
            _SituacionPatrimonial = new LibroDto();
            _Informacion = new LibroDto();
            _IVA = new LibroDto();
            _IVACompras = new LibroDto();
            _IVAVentas = new LibroDto();
            _PatrimonioNeto = new LibroDto();
            _Memoria = new LibroDto();
            _Resultados = new LibroDto();

        }
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
            set { if (value == null) value = new LibroDto(); value.Tipo = "Administracion"; _Administracion = value; }
        }

        private LibroDto _Asamblea;
        public LibroDto Asamblea
        {
            get { return _Asamblea; }
            set { if (value == null) value = new LibroDto(); value.Tipo = "Asamblea"; _Asamblea = value; }

        }

        private LibroDto _AsistenciaAsamblea;
        public LibroDto AsistenciaAsamblea
        {
            get { return _AsistenciaAsamblea; }
            set { if (value == null) value = new LibroDto(); value.Tipo = "AsistenciaAsamblea"; _AsistenciaAsamblea = value; }

        }

        private LibroDto _SituacionPatrimonial;
        public LibroDto SituacionPatrimonial
        {
            get { return _SituacionPatrimonial; }
            set { if (value == null) value = new LibroDto(); value.Tipo = "SituacionPatrimonial"; _SituacionPatrimonial = value; }

        }

        private LibroDto _Resultados;


        public LibroDto Resultados
        {
            get { return _Resultados; }
            set { if (value == null) value = new LibroDto(); value.Tipo = "Resultados"; _Resultados = value; }
        }

        private LibroDto _PatrimonioNeto;
        public LibroDto PatrimonioNeto
        {
            get { return _PatrimonioNeto; }
            set { if (value == null) value = new LibroDto(); value.Tipo = "PatrimonioNeto"; _PatrimonioNeto = value; }
        }

        private LibroDto _Efectivo;
        public LibroDto Efectivo
        {
            get { return _Efectivo; }
            set { if (value == null) value = new LibroDto(); value.Tipo = "Efectivo"; _Efectivo = value; }
        }

        private LibroDto _Informacion;
        public LibroDto Informacion
        {
            get { return _Informacion; }
            set { if (value == null) value = new LibroDto(); value.Tipo = "Informacion"; _Informacion = value; }

        }

        private LibroDto _EstadosContablesConsolidados;
        public LibroDto EstadosContablesConsolidados
        {
            get { return _EstadosContablesConsolidados; }
            set { if (value == null) value = new LibroDto(); value.Tipo = "EstadosContablesConsolidados"; _EstadosContablesConsolidados = value; }
        }

        private LibroDto _Fiscalizacion;
        public LibroDto Fiscalizacion
        {
            get { return _Fiscalizacion; }
            set { if (value == null) value = new LibroDto(); value.Tipo = "Fiscalizacion"; _Fiscalizacion = value; }
        }

        private LibroDto _Auditor;
        public LibroDto Auditor
        {
            get { return _Auditor; }
            set { if (value == null) value = new LibroDto(); value.Tipo = "Auditor"; _Auditor = value; }
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
            set { if (value == null) value = new LibroDto(); value.Tipo = "IVACompras"; _IVACompras = value; }
        }

        private LibroDto _IVAVentas;
        public LibroDto IVAVentas
        {
            get { return _IVAVentas; }
            set { if (value == null) value = new LibroDto(); value.Tipo = "IVAVentas"; _IVAVentas = value; }
        }

    }
}
