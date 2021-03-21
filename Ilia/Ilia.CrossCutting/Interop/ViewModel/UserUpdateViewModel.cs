using Ilia.CrossCutting.Interop.Base;

namespace Ilia.CrossCutting.Interop.ViewModel
{
    public class UserUpdateViewModel : BaseUpdateViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}