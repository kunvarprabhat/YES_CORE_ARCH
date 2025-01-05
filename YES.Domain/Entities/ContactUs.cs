using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static YES.Domain.BaseEntity;

namespace YES.Domain.Entities
{
#nullable disable
    public class ContactUs : DeleteEntity<int>
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Message { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
