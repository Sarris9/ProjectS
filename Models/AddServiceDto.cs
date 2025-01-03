using ProjectS.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace ProjectS.Models
{
    public class AddServiceDto
    {
        [Required]
        public Mark Mark { get; set; }
        [Required]
        public string Type { get; set; }
        public string? PlateNumber { get; set; }
        [Required]
        public int Kw { get; set; }
        public string DateOfService { get; set; }
        [Required]
        public string ServiceType { get; set; }

    }
}
