using Ilia.Domain.Base;

namespace Ilia.Domain
{
    public class User : BaseEntity
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}