using Balances.DTO;
using Balances.Services.Contract;
using Balances.Utilities;
using Microsoft.AspNetCore.Http;

namespace Balances.Services.Implementation
{
    public class ArchivoService : IArchivoService
    {
        private readonly IBalanceService _balanceService;


        public ArchivoService(IBalanceService balanceService)
        {
            _balanceService = balanceService;
        }

        const string _baseDir = @"c:\datos\balances";


        public void Delete(string balanceId, string archivoId)
        {
            //BORRAR ARCHIVO DEL FILESISTEM
            var balance = _balanceService.GetById(balanceId);
            var archivo = balance.Archivos.FirstOrDefault(x => x.Id == archivoId);
            if (archivo != null)
            {
                var fecha = archivo.FechaCreacion;
                var Periodo = CalcularPeriodo(fecha);

                var ArchivoABorrar = _baseDir + "/" + Periodo.ToString() + "/" +
                    archivo.Id.ToString() + Path.GetExtension(archivo.NombreArchivo);

                File.Delete(ArchivoABorrar);

            }
            //BORRAR ARCHIVO DE LA BASE
            _balanceService.DeleteArchivoBalance(balanceId, archivoId);
        }

        public bool Upload(string balanceId, IFormFileCollection files)
        {
            var listaArchivos = new List<ArchivoDTO>();
            var balance = _balanceService.GetById(balanceId);

            if (balance == null)
            {
                return false;
            }

            foreach (var file in files)
            {


                var newFile = new ArchivoDTO
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

                        var hash = HashSHA256.CalcularHash(fullPath);
                        newFile.Hash = hash;
                    }



                    listaArchivos.Add(newFile);
                    //balance.Archivos.Add(newFile);

                }
                balance.Archivos = (ICollection<Model.Archivo>)listaArchivos;
            }
            _balanceService.UpdateBalance(balance);
            return true;
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
