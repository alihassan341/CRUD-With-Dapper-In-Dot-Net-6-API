using System.ComponentModel.DataAnnotations;

namespace CRUDwithDapper.Model
{
    public class Users
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public  string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        
        public string Email { get; set; } = string.Empty;
        public string Cnic { get; set; } = string.Empty;        
        public string Password { get; set; } = string.Empty;
        public bool Admin { get; set; }
    }
}

