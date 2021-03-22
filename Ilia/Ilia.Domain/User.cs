using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Ilia.Domain.Base;

namespace Ilia.Domain
{
    public class User : BaseEntity
    {
        [Required]
        [Column(TypeName = "VARCHAR(40)")]
        [StringLength(40)]
        public string Username { get; set; }
        [Required]
        [Column(TypeName = "VARCHAR(40)")]
        [StringLength(40)]
        public string Password { get; set; }
    }
}