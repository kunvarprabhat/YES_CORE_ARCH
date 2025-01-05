using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static YES.Domain.BaseEntity;

namespace YES.Domain.Entities
{
    public class Visitor
    {
        [Key]
        public int Id { get; set; }
        public string CountryName { get; set; }
        public string? State { get; set; }
        public string? City { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
        public string? IPAddress { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
