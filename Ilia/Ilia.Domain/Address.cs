using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Ilia.Domain.Base;

namespace Ilia.Domain
{
    public class Address : BaseEntity
    {
        public Guid CustomerId { get; set; }
        [Required]
        [Column(TypeName = "VARCHAR(60)")]
        [StringLength(60)]
        public string Street { get; set; }
        [Required]
        public int Number { get; set; }
        [Required]
        [Column(TypeName = "VARCHAR(60)")]
        [StringLength(60)]
        public string ZipCode { get; set; }
        [Required]
        [Column(TypeName = "VARCHAR(60)")]
        [StringLength(60)]
        public string City { get; set; }
        [Required]
        [Column(TypeName = "VARCHAR(60)")]
        [StringLength(60)]
        public string State { get; set; }
        [Required]
        [Column(TypeName = "VARCHAR(60)")]
        [StringLength(60)]
        public string Country { get; set; }
    }
}
