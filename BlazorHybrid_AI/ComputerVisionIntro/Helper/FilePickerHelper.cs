using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerVisionIntro.Helper
{

    //public static class FilePickerHelper
    //{
    //    public static string? PickImageAsync()
    //    {
    //        var dialog = new OpenFileDialog
    //        {
    //            Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp",
    //            Multiselect = false
    //        };

    //        return dialog.ShowDialog() == true
    //            ? dialog.FileName
    //            : null;
    //    }
    //}
    public static class MyHelper
    {
        public static string Normalize(string input)
        {
            return input
                .Trim()
                .Replace(".", "")
                .Replace(",", "");
        }

      public static bool IsNumber(string input)
        {
            return int.TryParse(Normalize(input), out _);
        }

       public static bool IsPrice(string input)
        {
            return int.TryParse(Normalize(input), out _)
                   && Normalize(input).Length >= 4; 
        }
    }

    public static class FilePickerHelper
    {
        public static async Task<string?> PickImageAsync()
        {
            try
            {
                var customFileType = new FilePickerFileType(
                    new Dictionary<DevicePlatform, IEnumerable<string>>
                    {
                        { DevicePlatform.iOS, new[] { "public.image" } },
                        { DevicePlatform.Android, new[] { "image/*" } },
                        { DevicePlatform.WinUI, new[] { ".jpg", ".jpeg", ".png", ".bmp" } },
                        { DevicePlatform.MacCatalyst, new[] { "public.image" } }
                    });

                var options = new PickOptions
                {
                    PickerTitle = "Select an image",
                    FileTypes = customFileType
                };

                var result = await FilePicker.Default.PickAsync(options);
                return result?.FullPath;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }

    public static class FileHelper
    {
        public static async Task<string> ToBase64ImageAsync(string filePath)
        {
            var bytes = await File.ReadAllBytesAsync(filePath);
            var base64 = Convert.ToBase64String(bytes);

            var ext = Path.GetExtension(filePath).ToLower();
            var mime = ext switch
            {
                ".png" => "image/png",
                ".jpg" or ".jpeg" => "image/jpeg",
                ".bmp" => "image/bmp",
                _ => "image/jpeg"
            };

            return $"data:{mime};base64,{base64}";
        }
    }
}
