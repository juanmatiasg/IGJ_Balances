using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balances.DTO
{
    public class UploadFilesDTO
    {
       public List<FileDTO> ListFile {get;set;}

        public UploadFilesDTO()
        {
            ListFile = new List<FileDTO>();
        }
    }
}
