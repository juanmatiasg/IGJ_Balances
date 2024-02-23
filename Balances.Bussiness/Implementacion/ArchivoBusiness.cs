using AutoMapper;
using Balances.Bussiness.Contrato;
using Balances.DTO;
using Balances.Model;
using Balances.Utilities;
using Microsoft.AspNetCore.Http;

namespace Balances.Bussiness.Implementacion
{
    public class ArchivoBusiness : IArchivoBusiness
    {
        private readonly IBalanceBusiness _balanceBusiness;
        private readonly IMapper _mapper;
        private const string _baseDir = @"c:\datos\balances";

        public ArchivoBusiness(IBalanceBusiness balanceBusiness, IMapper mapper)
        {
            _balanceBusiness = balanceBusiness;
            _mapper = mapper;
        }

        public ResponseDTO<BalanceDto> Delete(Archivo modelo)
        {
            var resultadoDto = new ResponseDTO<BalanceDto>();
            resultadoDto.IsSuccess = false;

            try
            {
                // RECUPERO BALANCE
                var bal = _balanceBusiness.BalanceActual;

                // BUSCO EL ARCHIVO EN EL BALANCE
                var archivo = bal.Archivos.FirstOrDefault(x => x.Id == modelo.Id);

                if (archivo != null)
                {
                    // If archivo is not null, proceed with deletion
                    bal.Archivos.Remove(archivo);

                    // Check if FechaCreacion is not null before using it
                    if (archivo.FechaCreacion!=null)
                    {
                        string Periodo = CalcularPeriodo(archivo.FechaCreacion);
                        string fullPath = $@"{_baseDir}\{Periodo}\{archivo.Id}{Path.GetExtension(archivo.NombreArchivo)}";

                        if (File.Exists(fullPath))
                            File.Delete(fullPath);

                        _balanceBusiness.Update(bal);

                        resultadoDto.IsSuccess = true;
                        resultadoDto.Message = "archivo eliminado correctamente";
                        resultadoDto.Result = bal;
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

        public ResponseDTO<BalanceDto> UploadFilesDTO(UploadFilesDTO ufDto)
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

                        File.WriteAllBytes(fullPath, file.DatosBinarios);

                        newFile.Hash = file.Hash;

                        listaArchivos.Add(newFile);
                    }
                }

                // Update the business object outside the loop
                bDto.Archivos = listaArchivos;
                _balanceBusiness.Update(bDto);

                respuesta.IsSuccess = true;
                respuesta.Result = bDto;
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
