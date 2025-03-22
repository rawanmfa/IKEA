using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.Services.Attachment
{
    public class AttachmentServices : IAttachmentServices
    {
        private readonly List<string> AllowedExtension = new() { ".jpg", ".jpeg", ".png" };
        private const int FileMaxSize = 2_097_152; //2 Mbs
        public string UploadFile(IFormFile file, string FolderName)
        {
            #region Validations
            var fileExtension = Path.GetExtension(file.FileName);
            if (!AllowedExtension.Contains(fileExtension))
                throw new Exception("Invalid File Extension Please Try Again ");
            if (file.Length > FileMaxSize)
                throw new Exception("Invalid File Size ");
            #endregion
            var FolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", FolderName);
            if (!Directory.Exists(FolderPath))
                Directory.CreateDirectory(FolderPath);
            var FileName = $"{Guid.NewGuid()}{fileExtension}";
            var FilePath = Path.Combine(FolderPath, FileName);
            using var fileStream = new FileStream(FilePath, FileMode.Create);
            file.CopyTo(fileStream);
            return FileName;
        }
        public bool Delete(string FilePath)
        {
            if (File.Exists(FilePath))
            {
                File.Delete(FilePath);
                return true;
            }
            return false;
        }

    }
}
