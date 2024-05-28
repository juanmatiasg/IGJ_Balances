using Balances.Bussiness.Contrato;
using Balances.DTO;
using Balances.Services.Contract;

namespace Balances.Bussiness.Implementacion
{
    public class ArchivoBusiness : IArchivoBusiness
    {
        private readonly IBalanceBusiness _balanceBusiness;

        private const string _baseDir = @"c:\datos\balances";
        private readonly ISessionService _sessionService;

        public ArchivoBusiness(IBalanceBusiness balanceBusiness, ISessionService sessionService)
        {
            _balanceBusiness = balanceBusiness;
            _sessionService = sessionService;
        }

        public ResponseDTO<BalanceDto> Delete(ArchivoDTO modelo)
        {
            var resultadoDto = new ResponseDTO<BalanceDto>();
            resultadoDto.IsSuccess = false;

            try
            {

                //RECUPERO SESION EN EL ARCHIVO

                var sesionid = modelo.SesionId;

                // RECUPERO BALANCE
                var id = _sessionService.GetBalanceId(sesionid);
                var bal = _balanceBusiness.GetById(id);
                //var bal = _balanceBusiness.BalanceActual;

                // BUSCO EL ARCHIVO EN EL BALANCE
                var archivo = bal.Result.Archivos.FirstOrDefault(x => x.Id == modelo.Id);

                if (archivo != null)
                {
                    // If archivo is not null, proceed with deletion
                    bal.Result.Archivos.Remove(archivo);

                    // Check if FechaCreacion is not null before using it
                    if (archivo.FechaCreacion != null)
                    {
                        string Periodo = CalcularPeriodo(archivo.FechaCreacion);
                        string fullPath = $@"{_baseDir}\{Periodo}\{archivo.Id}{Path.GetExtension(archivo.NombreArchivo)}";

                        if (System.IO.File.Exists(fullPath))
                            System.IO.File.Delete(fullPath);

                        _balanceBusiness.Update(bal.Result);

                        resultadoDto.IsSuccess = true;
                        resultadoDto.Message = "archivo eliminado correctamente";
                        resultadoDto.Result = bal.Result;
                    }
                    else
                    {
                        resultadoDto.Message = "FechaCreacion is null for the archivo.";
                    }
                }
                else
                {
                    resultadoDto.Message = "Archivo no encontrado en el balance.";
                }
            }
            catch (Exception ex)
            {
                resultadoDto.Message = ex.Message;
            }

            return resultadoDto;
        }




        private string CalcularPeriodo(DateTime fecha)
        {
            string Año = fecha.Year.ToString();
            string Mes = fecha.Month.ToString();

            return @"\" + Año + @"\" + Mes;


        }
        private string CalcularPeriodo()
        {
            return CalcularPeriodo(DateTime.Now);


        }

        public ResponseDTO<BalanceDto> UploadFilesDTO(List<ArchivoDTO> ufDto)
        {
            ResponseDTO<BalanceDto> respuesta = new ResponseDTO<BalanceDto>();
            ArchivoDTO newFile;

            //RECUPERO SESION EN EL ARCHIVO
            var archivo = ufDto.FirstOrDefault();
            var sesionid = archivo!.SesionId;

            //busqueda del balance
            var id = _sessionService.GetBalanceId(sesionid);
            var bDto = _balanceBusiness.GetById(id);

            var listaArchivos = new List<ArchivoDTO>();


            try
            {
                foreach (var file in ufDto)
                {
                    newFile = new ArchivoDTO
                    {

                        Id = Guid.NewGuid().ToString(),
                        FechaCreacion = DateTime.UtcNow,
                        Categoria = file.Categoria,
                        NombreArchivo = file.NombreArchivo,
                        ContentType = file.ContentType,
                        Tamaño = file.Tamaño / 1024

                    };

                    if (file.Tamaño > 0)
                    {
                        string Periodo = CalcularPeriodo();
                        Directory.CreateDirectory(_baseDir + Periodo);
                        string fullPath = $@"{_baseDir}\{Periodo}\{newFile.Id}{Path.GetExtension(file.NombreArchivo)}";

                        //File.WriteAllBytes(fullPath, file.DatosBinarios);

                        newFile.Hash = file.Hash;

                        listaArchivos.Add(newFile);

                    }
                }

                // Update the business object outside the loop
                bDto.Result.Archivos = ufDto;

                _balanceBusiness.Update(bDto.Result);

                respuesta.IsSuccess = true;
                respuesta.Result = bDto.Result;
                respuesta.Message = "Archivo(s) guardado(s) correctamente";
            }
            catch (Exception ex)
            {
                respuesta.Message = ex.Message;
            }

            return respuesta;
        }
    }
}
