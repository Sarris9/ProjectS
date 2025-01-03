using System.ComponentModel.DataAnnotations;

namespace ProjectS.Models.Entities
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        [Required]
        public required string Email { get; set; }
        [Required]
        public required string Password { get; set; }
        [Required]
        public required int Phone { get; set; }
    }
}
