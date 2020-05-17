using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipelines;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorInputFile;
using Microsoft.AspNetCore.Hosting;

namespace Blazor_Component.Data
{
    public interface IFileUpload
    {
       
        Task UploadAsync(MemoryStream file, string fileName);
      
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

    }

}

