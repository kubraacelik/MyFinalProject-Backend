using Core.Entities;

namespace Entities.DTOs
{
    public class UserForRegisterDto : IDto //sistemimize kayıt olmak için
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
