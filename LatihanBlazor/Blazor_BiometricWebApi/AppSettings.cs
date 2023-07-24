namespace Blazor_BiometricWebApi
{
    public class AppSettings
    {
        public HashSet<string> WebAuthnOrigins { get; set; }
        public string WebAuthnServerDomain { get; set; }
        public string WebAuthnServerName { get; set; }
    }
}
