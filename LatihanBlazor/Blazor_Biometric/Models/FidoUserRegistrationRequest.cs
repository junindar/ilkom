using Fido2NetLib;

using System.Text.Json.Serialization;

namespace Blazor_Biometric.Models
{
    public class FidoUserRegistrationRequest
    {
        [JsonPropertyName("makeCredentialOptions")]
        public string MakeCredentialOptions { get; set; }

        [JsonPropertyName("response")]
        public AuthenticatorAttestationRawResponse Response { get; set; }
    }
}
