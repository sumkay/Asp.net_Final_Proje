using System.ComponentModel.DataAnnotations;

namespace Dis.Models
{
    public class ProfileViewModel
    {
        public int? PatientId { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
