using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipelines;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorInputFile;
using Microsoft.AspNetCore.Hosting;

namespace Blazor_CRUD.Data
{
    public interface IFileUpload
    {
       
        Task UploadAsync(MemoryStream file, string fileName);
        Task UploadAsync2(IFileListEntry file);
    }

    public class FileUpload : IFileUpload
    {

        private IWebHostEnvironment _environment;

        public FileUpload(IWebHostEnvironment environment)
        {
            _environment = environment;
        }



       
        public async Task UploadAsync(MemoryStream file,string fileName)
        {
            var uploads = Path.Combine(_environment.WebRootPath, "images", fileName);
           
            await using FileStream fs = new FileStream(uploads, FileMode.Create, FileAccess.Write);
            file.WriteTo(fs);
        }

        public async Task UploadAsync2(IFileListEntry file)
        {
            var uploads = Path.Combine(_environment.WebRootPath, "images", file.Name);

            var ms = new MemoryStream();
            await file.Data.CopyToAsync(ms);
           await using (FileStream fs = new FileStream(uploads, FileMode.Create, FileAccess.Write))
            {
                ms.WriteTo(fs);
            }
        }
    }

}

