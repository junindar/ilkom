using System.ComponentModel.DataAnnotations;

namespace Blazor_BiometricPWA.Models.Requests
{
    public class UserLoginRequest
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }
    }
}
