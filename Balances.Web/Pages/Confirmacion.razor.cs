using global::System;
using global::System.Collections.Generic;
using global::System.Linq;
using global::System.Threading.Tasks;
using global::Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using Microsoft.JSInterop;
using Balances.Web;
using Balances.Web.Shared;
using Blazorise;
using Radzen;
using Radzen.Blazor;
using Balances.DTO;
using Balances.Model;
using Balances.Utilities;
using Balances.Web.Services.Contracts;
using Balances.Web.Services.Implementation;

namespace Balances.Web.Pages
{
    public partial class Confirmacion
    {
        List<string> mensajesError = new List<string>();
        // reference to the modal component
        private Modal modalRef;
        private Modal modalSuccess;
        [Parameter]
        public string? TipoEntidad { get; set; }

        [Parameter]
        public string? balid { get; set; } = null;

        private BalanceDto balance = new BalanceDto();
        private CaratulaDto caratula = new CaratulaDto();
        private AutoridadDto autoridades = new AutoridadDto();
        private EstadoContableDto estadoContable = new EstadoContableDto();
        private List<AutoridadDto> listAutoridades = new List<AutoridadDto>();
        private LibrosDto libros = new LibrosDto();
        private LibroDto libro = new LibroDto();
        private ContadorDto contador = new ContadorDto();
        private List<Archivo> listArchivo = new List<Archivo>();
        private SociosDto socios = new SociosDto();
        private List<PersonaHumanaDto> listPersonaHumana = new List<PersonaHumanaDto>();
        private List<PersonaJuridicaDto> listPersonaJuridica = new List<PersonaJuridicaDto>();
        protected override async void OnInitialized()
        {
            await Load();
            ValidarCampos();
            await base.OnInitializedAsync();
        }

        private async Task Load()
        {
            try
            {
                var balanceId = await sesionService.GetSessionBalanceId();
                balid = balanceId;
                if (balanceId != null)
                {
                    var response = await balanceService.getBalance(balanceId);
                    if (response.IsSuccess == true)
                    {
                        try
                        {
                            if (response.Result != null)
                            {
                                //TipoEntidad = response.Result.Caratula.Entidad.TipoEntidad;
                                setBalance(balance);
                                //Caratula
                                setCaratula(response.Result.Caratula);
                                //EstadoContable
                                //setEstadoContable(response.Result.EstadoContable);
                                this.estadoContable.tipoBalance = response.Result.EstadoContable.TipoBalance;
                                this.estadoContable.fechaInicio = response.Result.EstadoContable.FechaInicio;
                                this.estadoContable.fechaEstado = response.Result.EstadoContable.FechaEstado;
                                this.estadoContable.fechaAsamblea = response.Result.EstadoContable.FechaAsamblea;
                                this.estadoContable.fechaReunionDirectorio = response.Result.EstadoContable.FechaReunionDirectorio;
                                this.estadoContable.cajaYBancos = response.Result.EstadoContable.CajaYBancos;
                                this.estadoContable.inversionesActivoCorriente = response.Result.EstadoContable.InversionesActivoCorriente;
                                //this.estadoContable.otrosRubros = response.Result.EstadoContable.OtrosRubros;
                                // this.balance.Caratula = response.Result.Caratula;
                                this.libros.Asamblea = response.Result.Libros.Asamblea;
                                this.libros.Administracion = response.Result.Libros.Administracion;
                                this.libros.AsistenciaAsamblea = response.Result.Libros.AsistenciaAsamblea;
                                this.libros.Auditor = response.Result.Libros.Auditor;
                                this.libros.Efectivo = response.Result.Libros.Efectivo;
                                this.libros.Fiscalizacion = response.Result.Libros.Fiscalizacion;
                                this.libros.IVA = response.Result.Libros.IVA;
                                this.libros.IVACompras = response.Result.Libros.IVACompras;
                                this.libros.IVAVentas = response.Result.Libros.IVAVentas;
                                this.libros.Resultados = response.Result.Libros.Resultados;
                                this.libros.EstadosContablesConsolidados = response.Result.Libros.EstadosContablesConsolidados;
                                this.libros.PatrimonioNeto = response.Result.Libros.PatrimonioNeto;
                                this.libros.SituacionPatrimonial = response.Result.Libros.SituacionPatrimonial;
                                this.libros.Memoria = response.Result.Libros.Memoria;
                                this.libros.Informacion = response.Result.Libros.Informacion;
                                //Contador
                                setContador(response.Result.Contador);
                                //Autoridades
                                setAutoridades(response.Result.Autoridades);
                                //Integrante
                                this.listPersonaHumana = response.Result.Socios.PersonasHumanas;
                                this.listPersonaJuridica = response.Result.Socios.PersonasJuridicas;
                                //Archivos
                                //setArchivos(response.Result.Archivos);
                                StateHasChanged();
                            }
                            else
                            {
                                Console.WriteLine($"No hay resultados en la respuesta");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Hubo un problema con la solicitud fetch: {ex.Message}");
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"No se ha encontrado el idSession");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"SessionId: Hubo un problema con la solicitud fetch: {ex.Message}");
            }
        }

        private async Task<ResponseDTO<BalanceDto>> generarPresentacion()
        {
            try
            {
                await ShowLoadingModal();
                var responseDto = new ResponseDTO<BalanceDto>();
                var response = await presentacionService.generarPresentacion();
                if (response.IsSuccess)
                {
                    responseDto.IsSuccess = response.IsSuccess;
                    responseDto.Message = response.Message;
                    responseDto.Result = response.Result;
                    await ShowSuccessModal();
                    return responseDto;
                }
                else
                {
                    responseDto.IsSuccess = response.IsSuccess;
                    responseDto.Message = response.Message;
                    responseDto.Result = response.Result;
                    return responseDto;
                }
            }
            catch (Exception ex)
            {
                return new ResponseDTO<BalanceDto>
                {
                    IsSuccess = false,
                    Message = $"{ex.Message}",
                    Result = null
                };
            }
            finally
            {
                await HideLoadingModal();
            }
        }

        private void setCaratula(Model.Caratula caratula)
        {
            this.caratula.Entidad = caratula.Entidad;
            this.caratula.Entidad.Domicilio = caratula.Entidad.Domicilio;
            this.caratula.Entidad.RazonSocial = caratula.Entidad.RazonSocial;
            this.caratula.FechaInicio = caratula.FechaInicio;
            this.caratula.FechaDeCierre = caratula.FechaDeCierre;
            this.caratula.Entidad.TipoEntidad = caratula.Entidad.TipoEntidad;
            this.caratula.Entidad.Correlativo = caratula.Entidad.Correlativo;
            this.caratula.Entidad.SedeSocialInscripta = caratula.Entidad.SedeSocialInscripta;
            TipoEntidad = caratula.Entidad.TipoEntidad;
        }

        private void setEstadoContable(Model.EstadoContable estadoContable)
        {
            this.estadoContable.tipoBalance = estadoContable.TipoBalance;
            this.estadoContable.fechaInicio = estadoContable.FechaInicio;
            this.estadoContable.fechaEstado = estadoContable.FechaEstado;
            this.estadoContable.fechaAsamblea = estadoContable.FechaAsamblea;
            this.estadoContable.fechaReunionDirectorio = estadoContable.FechaReunionDirectorio;
            this.estadoContable.cajaYBancos = estadoContable.CajaYBancos;
            this.estadoContable.inversionesActivoCorriente = estadoContable.CajaYBancos;
        }

        private void setBalance(BalanceDto balance)
        {
            this.balance = balance;
            this.balance.Libros = new LibrosDto();
            this.balance.Libros.Asamblea = new LibroDto();
        }

        private void setAutoridades(List<AutoridadDto> lista)
        {
            this.listAutoridades = lista;
        }

        private void setLibros(LibrosDto libro)
        {
            this.libros = libro;
        }

        private void setArchivos(List<Archivo> archivos)
        {
            this.listArchivo = archivos;
        }

        private void setIntegranteHumana(List<PersonaHumanaDto> listPersonaHumana)
        {
            this.listPersonaHumana = listPersonaHumana;
        }

        private void setIntegranteJuridica(List<PersonaJuridicaDto> listPersonaJuridica)
        {
            this.listPersonaJuridica = listPersonaJuridica;
        }

        private void setContador(Model.Contador contador)
        {
            this.contador.Nombre = contador.Nombre;
            this.contador.Apellido = contador.Apellido;
            this.contador.TipoDocumento = contador.TipoDocumento;
            this.contador.NroDocumento = contador.NroDocumento;
            this.contador.NroFiscal = contador.NroFiscal;
            this.contador.Tomo = contador.Tomo;
            this.contador.Folio = contador.Folio;
            this.contador.NroLegalInfoAudExt = contador.NroLegalInfoAudExt;
            this.contador.FechaInformeAuditorExt = contador.FechaInformeAuditorExt;
        }

        private bool ValidarCampos()
        {
            try
            {
                mensajesError.Clear();
                // Validar propiedades de Entidad
                ValidarPropiedades(this.caratula.Entidad, "Entidad");
                //Validar propiedades de EstadoContable
                ValidarPropiedades(this.estadoContable, "Estado Contable");
                //Validar propiedades Libros
                ValidarPropiedades(this.libros.Memoria, "Libros - Memoria");
                ValidarPropiedades(this.libros.Administracion, "Libros - Acta del Organo de Administracion");
                ValidarPropiedades(this.libros.Asamblea, "Libros - Asamblea de Reunion Socios");
                ValidarPropiedades(this.libros.SituacionPatrimonial, "Libros - Estado de Situacion Patrimonial");
                ValidarPropiedades(this.libros.Resultados, "Libros - Estado de Resultados");
                ValidarPropiedades(this.libros.PatrimonioNeto, "Libros - Estado de Evolucion del Patrimonio Neto");
                ValidarPropiedades(this.libros.Efectivo, "Libros - Estado de Flujo Efectivo");
                ValidarPropiedades(this.libros.Informacion, "Libros - Informacion Complementaria");
                ValidarPropiedades(this.libros.EstadosContablesConsolidados, "Libros - Estado Contable Consolidados");
                ValidarPropiedades(this.libros.Fiscalizacion, "Libros - Informe Organo Fiscalizacion");
                ValidarPropiedades(this.libros.Auditor, "Libros - Informe Auditor Auditor");
                ValidarPropiedades(this.libros.AsistenciaAsamblea, "Libros - Registro de Asistencia");
                ValidarPropiedades(this.libros.IVA, "Libros - IVA");
                ValidarPropiedades(this.libros.IVACompras, "Libros - IVA Compras");
                ValidarPropiedades(this.libros.IVAVentas, "Libros - IVA Ventas");
                //Validar propiedades de Contador
                ValidarPropiedades(this.contador, "Contador");
                // Validar propiedades de Autoridades
                ValidarPropiedades(this.listAutoridades, "Autoridades");
                var listaSocios = new List<object>();
                listaSocios.AddRange(this.listPersonaJuridica);
                listaSocios.AddRange(this.listPersonaHumana);
                //Validar propiedades de Socios
                if (!(listaSocios.Count > 0))
                {
                    ValidarPropiedades(listaSocios, "Integrante");
                }

                // Validar propiedades de Archivos
                ValidarPropiedades(this.listArchivo, "Archivos");
                // Verificar si hay mensajes de error y mostrarlos
                if (mensajesError.Any())
                { // Limpiar la lista de mensajes de error antes de cada validación
                    StateHasChanged();
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Validar Campos: " + ex.Message);
            }

            return true;
        }

        private string GetTitulo(string mensaje)
        {
            // Implement your logic to classify errors based on the object
            if (mensaje.Contains("Contador"))
            {
                return "Contador";
            }
            else if (mensaje.Contains("Libros - Memoria"))
            {
                return "Libros - Memoria";
            }
            else if (mensaje.Contains("Libros - Acta del Organo de Administracion"))
            {
                return "Libros - Acta del Organo de Administracion";
            }
            else if (mensaje.Contains("Libros - Asamblea de Reunion Socios"))
            {
                return "Libros - Asamblea de Reunion Socios";
            }
            else if (mensaje.Contains("Libros - Estado de Situacion Patrimonial"))
            {
                return "Libros - Estado de Situacion Patrimonial";
            }
            else if (mensaje.Contains("Libros - Estado de Resultados"))
            {
                return "Libros - Estado de Resultados";
            }
            else if (mensaje.Contains("Libros - Estado de Evolucion del Patrimonio Neto"))
            {
                return "Libros - Estado de Evolucion del Patrimonio Neto";
            }
            else if (mensaje.Contains("Libros - Estado de Flujo Efectivo"))
            {
                return "Libros - Estado de Flujo Efectivo";
            }
            else if (mensaje.Contains("Libros - Informacion Complementaria"))
            {
                return "Libros - Informacion Complementaria";
            }
            else if (mensaje.Contains("Libros - Estados Contable Consolidado"))
            {
                return "Libros - Estados Contables Consolidados";
            }
            else if (mensaje.Contains("Libros - Informe Organo Fiscalizacion"))
            {
                return "Libros - Informe Organo Fiscalizacion";
            }
            else if (mensaje.Contains("Libros - Informe Auditor Auditor"))
            {
                return "Libros - Informe Auditor Auditor";
            }
            else if (mensaje.Contains("Libros - Registro de Asistencia"))
            {
                return "Libros - Registro de Asistencia";
            }
            else if (mensaje.Contains("Libros - IVA"))
            {
                return "Libros - IVA";
            }
            else if (mensaje.Contains("Libros - IVA Ventas"))
            {
                return "Libros - IVA Ventas";
            }
            else if (mensaje.Contains("Libros - IVA Compras"))
            {
                return "Libros - IVA Compras";
            }
            //Autoridades
            else if (mensaje.Contains("Autoridades"))
            {
                return "Autoridades";
            }
            //Integrantes
            else if (mensaje.Contains("Integrante"))
            {
                return "Integrante";
            }
            else if (mensaje.Contains("Estado Contable"))
            {
                return "Estado Contable";
            }
            else if (mensaje.Contains("Archivos"))
            {
                return "Archivos";
            }
            // Add more conditions as needed
            else
            {
                return "Otros Errores";
            }
        }

        private string GetMensaje(string mensaje)
        {
            // Extract the error message without the title
            var startIndex = mensaje.IndexOf(":") + 1; // Find the index after the colon
            if (startIndex > 0 && startIndex < mensaje.Length)
            {
                return mensaje.Substring(startIndex).Trim(); // Extract the message and remove leading/trailing spaces
            }
            else
            {
                return mensaje; // Return the original message if the format is unexpected
            }
        }

        private void ValidarPropiedades(object entidad, string nombreEntidad)
        {
            try
            {
                // Bandera para verificar si ya se ha agregado un mensaje para la entidad
                bool mensajeAgregado = false;
                foreach (var propiedad in entidad.GetType().GetProperties())
                {
                    var valor = propiedad.GetValue(entidad);
                    if (propiedad.Name == "id" || propiedad.Name == "Id" || propiedad.Name == "Balance")
                    {
                        continue;
                    }

                    if (propiedad.Name == "FechaUltimaRegistracion" || propiedad.Name == "FechaRubrica")
                    {
                        continue;
                    }

                    if (valor == null)
                    {
                        mensajesError.Add($"La propiedad {propiedad.Name} en {nombreEntidad} está vacía");
                    }
                    else if (valor is string && string.IsNullOrEmpty((string)valor))
                    {
                        mensajesError.Add($"La propiedad {propiedad.Name} en {nombreEntidad} está vacía");
                    }
                    else if (valor is int && (int)valor == 0 && propiedad.Name != "Count" && propiedad.Name != "Capacity")
                    {
                        mensajesError.Add($"La propiedad {propiedad.Name} en {nombreEntidad} debe ser un valor numérico");
                    }
                    // Si la propiedad es Count o Capacity y el valor es cero, y no se ha agregado mensaje
                    else if ((propiedad.Name == "Count" || propiedad.Name == "Capacity") && valor is int && (int)valor == 0 && !mensajeAgregado)
                    {
                        mensajesError.Add($"{nombreEntidad} está vacía");
                        // Establecer la bandera para indicar que se ha agregado el mensaje
                        mensajeAgregado = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error:Validar Propiedades" + ex.Data.ToString());
                Console.WriteLine("Error:Validar Propiedades" + ex.Message);
            }
        }

        private decimal resultadoPatrimonioNeto()
        {
            if (estadoContable.totalActivo != 0 && estadoContable.totalPasivo != 0)
            {
                return estadoContable.patrimonioNeto = estadoContable.totalActivo - estadoContable.totalPasivo;
            }
            else
            {
                return 0;
            }
        }

        private bool resultadoPatNetDetailPatNeto()
        {
            if (estadoContable.otrosRubros.Count > 0)
            {
                decimal sumatoriaImporte = 0;
                foreach (var rubros in estadoContable.otrosRubros)
                {
                    sumatoriaImporte = sumatoriaImporte + rubros.importe;
                }

                var sumatoriaDetallePatNeto = estadoContable.capitalSuscripto + estadoContable.ajusteCapital + estadoContable.aportesIrrevocables + estadoContable.primaEmision + estadoContable.gananciasReservadas + estadoContable.perdidasAcumuladas + estadoContable.gananciasPerdidasEjercicio + estadoContable.reservaLegal + sumatoriaImporte;
                if (resultadoPatrimonioNeto().Equals(sumatoriaDetallePatNeto))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                var sumatoriaDetallePatNeto = estadoContable.capitalSuscripto + estadoContable.ajusteCapital + estadoContable.aportesIrrevocables + estadoContable.primaEmision + estadoContable.gananciasReservadas + estadoContable.perdidasAcumuladas + estadoContable.gananciasPerdidasEjercicio + estadoContable.reservaLegal;
                if (resultadoPatrimonioNeto().Equals(sumatoriaDetallePatNeto))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        // Método para mostrar el modal de carga
        private Task ShowLoadingModal()
        {
            return modalRef.Show();
        }

        // Método para ocultar el modal de carga
        private Task HideLoadingModal()
        {
            return modalRef.Hide();
        }

        private Task ShowSuccessModal()
        {
            return modalSuccess.Show();
        }
    }
}