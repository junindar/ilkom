using System.ComponentModel.DataAnnotations;

namespace Blazor_BiometricPWA.Models.Requests
{
    public class UserRegistrationRequest
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Display Name is required")]
        public string DisplayName { get; set; }
    }
}
