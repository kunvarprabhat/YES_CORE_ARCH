namespace YES.Dtos.Common
{
    public class BaseDto
    {
        public int BranchId { get; set; }
        public int BranchType { get; set; }
        public string? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
