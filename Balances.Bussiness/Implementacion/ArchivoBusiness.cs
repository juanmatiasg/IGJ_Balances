using AutoMapper;
using Balances.Bussiness.Contrato;
using Balances.DTO;
using Balances.Model;
using Balances.Services.Contract;
using Microsoft.Extensions.Logging;

namespace Balances.Bussiness.Implementacion
{
    public class ArchivoBusiness : IArchivoBusiness
    {
        private readonly IBalanceBusiness _balanceBusiness;
        private readonly IMapper _mapper;
        private const string _baseDir = @"c:\datos\balances";
        private readonly ILogger<ArchivoBusiness> _logger;
        private readonly ISessionService _sessionService;

        public ArchivoBusiness(IBalanceBusiness balanceBusiness,
                               ISessionService sessionService,
                               IMapper mapper,
                               ILogger<ArchivoBusiness> logger)
        {
            _balanceBusiness = balanceBusiness;
            _mapper = mapper;
            _logger = logger;
            _sessionService = sessionService;
        }

        public ResponseDTO<BalanceDto> Delete(ArchivoDTO modelo)
        {

            var resultadoDto = new ResponseDTO<BalanceDto>();
            resultadoDto.IsSuccess = false;
            try
            {

                //RECUPERO BALANCE
                var bal = _balanceBusiness.BalanceActual;


                //BUSCO EL ARCHIVO EN EL BALANCE
                var archivo = bal.Archivos.FirstOrDefault(x => x.Id == modelo.Id);

                if (archivo != null) bal.Archivos.Remove(archivo);

                string Periodo = CalcularPeriodo(archivo.FechaCreacion);
                string fullPath = $@"{_baseDir}\{Periodo}\{archivo.Id}{Path.GetExtension(archivo.NombreArchivo)}";

                if (File.Exists(fullPath))
                    File.Delete(fullPath);

                _balanceBusiness.Update(bal);

                resultadoDto.IsSuccess = true;
                resultadoDto.Message = "archivo eliminado correctamente";
                resultadoDto.Result = bal;
                _logger.LogInformation("ArchivoBusiness.Delete: correcto");
            }
            catch (Exception ex)
            {

                resultadoDto.Message = ex.Message;
                _logger.LogError($"ArchivoBusiness.Delete: \n {ex}");
            }

            return resultadoDto;
        }

        public ResponseDTO<BalanceDto> Upload(UploadFilesDTO ufDto)
        {
            ResponseDTO<BalanceDto> respuesta = new ResponseDTO<BalanceDto>();

            var bDto = _balanceBusiness.BalanceActual;

            var listaArchivos = new List<Archivo>();
            try
            {
                foreach (var file in ufDto.ListFile)
                {


                    var newFile = new Archivo
                    {
                        Id = Guid.NewGuid().ToString(),
                        FechaCreacion = DateTime.Now,
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

                        File.WriteAllBytes(fullPath, file.DatosBinarios);

                        newFile.Hash = file.Hash;

                        listaArchivos.Add(newFile);
                        //balance.Archivos.Add(newFile);

                    }

                    bDto.Archivos = listaArchivos;
                    _balanceBusiness.Update(bDto);
                    respuesta.IsSuccess = true;
                    respuesta.Result = bDto;
                    respuesta.Message = "archivo guardado correctamente";
                    _logger.LogInformation("ArchivoBusiness.Upload: correcto");
                }
            }
            catch (Exception ex)
            {

                respuesta.Message = ex.Message;
                _logger.LogError($"ArchivoBusiness.Upload: \n {ex}");
            }

            return respuesta;
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
    }
}
