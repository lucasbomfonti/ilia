using Ilia.Domain.Base;
using Ilia.Domain.Enum;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ilia.Domain
{
    public class PhoneContact : BaseEntity
    {
        [Required]
        public Guid CustomerId { get; set; }
        [Required]
        [Column(TypeName = "VARCHAR(40)")]
        [StringLength(40)]
        public string Number { get; set; }
        public EnumPhoneType PhoneType { get; set; }
    }
}