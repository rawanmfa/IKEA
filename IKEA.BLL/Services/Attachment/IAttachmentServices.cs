using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.Services.Attachment
{
    public interface IAttachmentServices
    {
        string UploadFile(IFormFile file, string FolderName);
        bool Delete(string FilePath);
    }
}
