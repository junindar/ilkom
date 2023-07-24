using Fido2NetLib;

namespace Blazor_BiometricWebApi.Models.Requests
{
    public class FidoUserRegistrationRequest
    {
        public string MakeCredentialOptions { get; set; }
        public AuthenticatorAttestationRawResponse Response { get; set; }
    }
}
