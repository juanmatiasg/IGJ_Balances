using Balances.DTO;
using Balances.Model;

namespace Balances.Services.Contract
{
    public class LibrosDigitalesService
    {

        private readonly IBalanceService _balanceService;

        public LibrosDigitalesService(IBalanceService balanceService)
        {
            _balanceService = balanceService;
        }

        public void /*string*/ ActualizarLibrosDigitales(LibrosDigitalesDto librosDigitales)
        {
            var balance = _balanceService.GetById(librosDigitales.BalanceId);

            balance.LibrosDigitales = librosDigitales.librosDigitales.Select(x => x.GetLibroDigital()).ToList();
            //balance.LibrosDigitalesEstado = _validation.GetEstado(balance.LibrosDigitales);
            _balanceService.UpdateBalance(balance.Id, balance);


        }

        public LibrosDigitalesDto Get(string balanceId)
        {
            var balance = _balanceService.GetById(balanceId);

            var librosDigitales = new LibrosDigitalesDto();
            librosDigitales.librosDigitales = new List<LibroDigitalDto>();


            librosDigitales.BalanceId = balanceId;
            balance.LibrosDigitales.ForEach(x => librosDigitales.librosDigitales.Add(new LibroDigitalDto(x)));


            return librosDigitales;
        }

        public List<LibroDigital> GetNewList()
        {
            var list = new List<LibroDigital>
            {

                new LibroDigital {
                     TipoDocumento= "MEMORIA",
                     Denominacion= "Memoria",
                     Nombre="",
                     NumeroRl= "",
                     FechaUltimaRegistracion= DateTime.Now,
                     Folio= ""
                   },

                   new LibroDigital {
                     TipoDocumento= "ACTA_ORGANO",
                     Denominacion= "Acta Organo Administracion",
                     Nombre="",
                     NumeroRl= "",
                     FechaUltimaRegistracion= DateTime.Now,
                     Folio= ""
                   },


                   new LibroDigital {
                     TipoDocumento= "ACTA_ASAMBLEA",
                     Denominacion= "Acta de Asamblea",
                     Nombre="",
                     NumeroRl= "",
                     FechaUltimaRegistracion= DateTime.Now,
                     Folio= ""
                   },


                   new LibroDigital {
                     TipoDocumento= "REGISTRO_ASISTENCIA",
                     Denominacion= "Registro de Asistencia a Asamblea",
                     Nombre="",
                     NumeroRl= "",
                     FechaUltimaRegistracion= DateTime.Now,
                     Folio= ""
                   },
                   new LibroDigital {
                     TipoDocumento= "SITUACION_PATRIMONIAL",
                     Denominacion= "Estado de Situacion Patrimonial",
                     Nombre="",
                     NumeroRl= "",
                     FechaUltimaRegistracion= DateTime.Now,
                     Folio= ""

                   },
                   new LibroDigital {
                     TipoDocumento= "ESTADO_RESULTADO",
                     Denominacion= "Estado de Resultados",
                     Nombre="",
                     NumeroRl= "",
                     FechaUltimaRegistracion= DateTime.Now,
                     Folio= ""
                   },
                   new LibroDigital {
                     TipoDocumento= "EVOLUCION_PATRIMONIO",
                     Denominacion= "Estado de Evolución de Patrimonio Neto",
                     Nombre="",
                     NumeroRl= "",
                     FechaUltimaRegistracion= DateTime.Now,
                     Folio= ""
                   },
                   new LibroDigital {
                     TipoDocumento= "FLUJO_EFECTIVO",
                     Denominacion= "Estado de Flujo de Efectivo",
                     Nombre="",
                     NumeroRl= "",
                     FechaUltimaRegistracion= DateTime.Now,
                     Folio= ""
                   },

                   new LibroDigital {
                     TipoDocumento= "INFO_COMPLEMENTARIA",
                     Denominacion= "Información Complementaria",
                     Nombre="",
                     NumeroRl= "",
                     FechaUltimaRegistracion= DateTime.Now,
                     Folio= ""
                   },
                   new LibroDigital {
                     TipoDocumento= "ESTADOS_CONTABLES",
                     Denominacion= "Estados Contabes Consolidados",
                     Nombre="",
                     NumeroRl= "",
                     FechaUltimaRegistracion= DateTime.Now,
                     Folio= ""
                   },
                   new LibroDigital {
                     TipoDocumento= "INFORME_ORGANO",
                     Denominacion= "Informe Organo Fiscalizacion",
                     Nombre="",
                     NumeroRl= "",
                     FechaUltimaRegistracion= DateTime.Now,
                     Folio= ""
                   },
                   new LibroDigital {
                     TipoDocumento= "INFORME_AUDITOR",
                     Denominacion= "Informe de Auditor Externo",
                     Nombre="",
                     NumeroRl= "",
                     FechaUltimaRegistracion= DateTime.Now,
                     Folio= ""


                   },


                new LibroDigital {
                     TipoDocumento= "LIBRO_DIARIO",
                     Denominacion= "",
                     Nombre="Libro IVA",
                     NumeroRl= "",
                     FechaUltimaRegistracion= DateTime.Now,
                     Folio= ""

                },

                      new LibroDigital {
                     TipoDocumento= "IVA_COMPRAS",
                     Denominacion= "",
                     Nombre="IVA Compras",
                     NumeroRl= "",
                     FechaUltimaRegistracion= DateTime.Now,
                     Folio= ""

                },

                     new LibroDigital {
                     TipoDocumento= "IVA_VENTAS",
                     Denominacion= "",
                     Nombre="IVA Ventas",
                     NumeroRl= "",
                     FechaUltimaRegistracion= DateTime.Now,
                     Folio= ""

                },


            };
            return list;
        }
    }
}
