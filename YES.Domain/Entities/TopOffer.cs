using System.ComponentModel.DataAnnotations;
using static YES.Domain.BaseEntity;

namespace YES.Domain.Entities
{
    public class TopOffer : AuditEntity<int>
    {
        [Key]
        public int Id { get; set; }
        public string OfferText { get; set; }
        public bool IsActive { get; set; }

    }
}
