
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;


namespace ComputerVisionIntro.Service
{
    public interface IComputerVisionService
    {
        Task<string> AnalyzeImageAsync(string imagePath);

        Task<(List<string> Lines, string Total, string Date, string Merchant, byte[] ImageData)> AnalyzeReceiptAsync(
            Stream imageStream);
    }
   
}


