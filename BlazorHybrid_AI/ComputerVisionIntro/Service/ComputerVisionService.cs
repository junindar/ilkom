

using System.Text.RegularExpressions;
using Azure;
using Azure.AI.Vision.ImageAnalysis;


namespace ComputerVisionIntro.Service
{
    public class ComputerVisionService: IComputerVisionService
    {
        private readonly string _subscriptionKey = ""; 
        private readonly string _endpoint = "https://****.cognitiveservices.azure.com/"; 

        private readonly ImageAnalysisClient _client;

        public ComputerVisionService()
        {
           

            _client = new ImageAnalysisClient(
                new Uri(_endpoint),
                new AzureKeyCredential(_subscriptionKey));
        }

      

        public async Task<string> AnalyzeImageAsync(string imagePath)
        {

           
            using var imageStream = new FileStream(imagePath, FileMode.Open);
            var imageData = BinaryData.FromStream(imageStream);
            var options = new ImageAnalysisOptions
            {
                Language = "en"
            };

            var result = await _client.AnalyzeAsync(
                imageData,
                VisualFeatures.Caption,
                options);
            if (!result.HasValue || result.Value.Caption == null)
            {
                return "Tidak ada deskripsi";
            }

            var caption = result.Value.Caption.Text;
            return caption;


          
        }


        public async Task<(List<string> Lines, string Total, string Date, string Merchant, byte[] ImageData)> AnalyzeReceiptAsync(Stream imageStream)
        {
            byte[] imageBytes;
            using (var ms = new MemoryStream())
            {
                await imageStream.CopyToAsync(ms);
                imageBytes = ms.ToArray();
            }

          
            var imageData = BinaryData.FromBytes(imageBytes);

            var options = new ImageAnalysisOptions
            {
                Language = "en" 
            };

           
            var result = await _client.AnalyzeAsync(imageData, VisualFeatures.Read, options);

            if (!result.HasValue || result.Value?.Read?.Blocks == null)
            {
                return (new List<string>(), "", "", "", imageBytes);
            }

           
            var lines = result.Value.Read.Blocks
                .SelectMany(block => block.Lines)
                .Select(line => line.Text.Trim())
                .ToList();

            int totalIndex = lines.FindIndex(l => l.Trim().Equals("Total:"));

            string total = totalIndex >= 0 && totalIndex + 1 < lines.Count
                ? lines[totalIndex + 1]
                : "";


            string date = lines
                .FirstOrDefault(l => l.StartsWith("Tanggal:"))
                ?.Replace("Tanggal:", "")
                .Trim();

         

            string merchant = lines
                .FirstOrDefault(l => !string.IsNullOrWhiteSpace(l) && !Regex.IsMatch(l, @"[\d.,]+")) ?? "";

            return (lines, total, date, merchant, imageBytes);
        }

      
    }
}
