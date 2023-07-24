using Fido2NetLib.Development;

namespace Blazor_BiometricWebApi.DataAccess.Models
{
    public class User : BaseModel
    {
        public int UserID { get; set; }
        public uint Counter { get; set; }
        public byte[] CredentialID { get; set; }
        public byte[] PublicKey { get; set; }
        public string Username { get; set; }
        public string DisplayName { get; set; }
        public string CredType { get; set; }
        public byte[] UserHandle { get; set; }
    }
}
