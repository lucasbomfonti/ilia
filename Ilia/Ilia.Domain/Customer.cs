using Ilia.Domain.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ilia.Domain
{
    public class Customer : BaseEntity
    {
        [Required]
        [Column(TypeName = "VARCHAR(40)")]
        [StringLength(40)]
        public string Name { get; set; }
        [Required]
        [Column(TypeName = "VARCHAR(40)")]
        [StringLength(40)]
        public string Email { get; set; }
        public virtual ICollection<PhoneContact> PhoneContact { get; set; }
        public virtual ICollection<Address> Address { get; set; }
    }
}