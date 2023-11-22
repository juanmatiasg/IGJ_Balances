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

        public ResponseDTO<BalanceDto> Delete(ArchivoDTO modelo)
        {
            throw new NotImplementedException();
        }

        public ResponseDTO<BalanceDto> Upload(IFormFileCollection files)
        {
            ResponseDTO<BalanceDto> respuesta = new ResponseDTO<BalanceDto>();
            var bDto = _balanceBusiness.BalanceActual;
            var listaArchivos = new List<Archivo>();
            try
            {
                foreach (var file in files)
                {


                    var newFile = new Archivo
                    {
                        Id = Guid.NewGuid().ToString(),
                        FechaCreacion = DateTime.Now,
                        Categoria = file.Name,
                        NombreArchivo = file.FileName,
                        ContentType = file.ContentType,
                        Tamaño = file.Length / 1024
                    };

                    if (file.Length > 0)
                    {
                        string Periodo = CalcularPeriodo();
                        Directory.CreateDirectory(_baseDir + Periodo);
                        string fullPath = $@"{_baseDir}\{Periodo}\{newFile.Id}{Path.GetExtension(file.FileName)}";
                        using (var ms = new MemoryStream())
                        {
                            file.CopyTo(ms);

                            File.WriteAllBytes(fullPath, ms.ToArray());

                            var hash = HashSha256.CalcularHash(fullPath);
                            newFile.Hash = hash;
                        }



                        listaArchivos.Add(newFile);
                        //balance.Archivos.Add(newFile);

                    }
                    bDto.Archivos = listaArchivos;
                    _balanceBusiness.Update(bDto);
                    respuesta.IsSuccess = true;
                    respuesta.Result = bDto;
                    respuesta.Message = "archivo guardado correctamente";
                }
            }
            catch (Exception ex)
            {

                respuesta.Message = ex.Message;
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
