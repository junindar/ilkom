namespace Blazor_BiometricWebApi.DataAccess.Models
{
    public class BaseModel
    {
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
