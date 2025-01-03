using System.ComponentModel.DataAnnotations;

namespace ProjectS.Models
{
    public class UpdateCustomerDto
    {
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
