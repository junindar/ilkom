namespace Blazor_Biometric.Entity
{
    public class User
    {
        public int UserID { get; set; }
        public uint Counter { get; set; }
        public byte[] CredentialID { get; set; }
        public byte[] PublicKey { get; set; }
        public string Username { get; set; }
        public string DisplayName { get; set; }
        public string CredType { get; set; }
        public byte[] UserHandle { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime AddedOn { get; set; } = DateTime.UtcNow;
        public string? AddedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string? UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string? DeletedBy { get; set; }
    }
}
