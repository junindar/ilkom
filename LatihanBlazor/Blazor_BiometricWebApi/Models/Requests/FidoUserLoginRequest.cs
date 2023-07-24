using Fido2NetLib;

namespace Blazor_BiometricWebApi.Models.Requests
{
    public class FidoUserLoginRequest
    {
        public string MakeAssertionOptions { get; set; }
        public AuthenticatorAssertionRawResponse Response { get; set; }
    }
}
