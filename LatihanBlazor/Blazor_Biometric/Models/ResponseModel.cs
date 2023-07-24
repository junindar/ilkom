using Fido2NetLib;

namespace Blazor_Biometric.Models
{
    public class ResponseModel
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }
    public class RegistrationResponseModel
    {
        public string attestationOptions { get; set; }
        public CredentialCreateOptions credentialCreateOptions { get; set; }

        public object responseObject { get; set; }
    }
}
