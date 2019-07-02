
namespace Communication.BL.Models
{
    public class UserModel : ModelBase
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string TelephoneNumber { get; set; }
        public byte[] Photo { get; set; }
        public bool isEnabled { get; set; }
    }
}
