namespace Balances.DTO
{
    public class UploadFilesDTO
    {
        public List<FileDTO> ListFile { get; set; }

        public UploadFilesDTO()
        {
            ListFile = new List<FileDTO>();
        }
    }
}
