using YES.Dtos.Common;

namespace YES.Dtos.TopMessage
{
    public class TopMessageDto : BaseDto
    {
        public int Id { get; set; }
        public string OfferText { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
