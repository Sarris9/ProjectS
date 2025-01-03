using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectS.Models.Entities
{
    public enum Mark { Honda, Yamaha, Suzuki, Piaggio, Sym, Kymco, Other };
    public class Service
    {
        [Key]
        public int ServiceId { get; set; }
        [ForeignKey("Customer")]
        public int Id { get; set; }
        [Required]
        public  Mark Mark { get; set; }
        [Required]
        public  string Type { get; set; }
        public string? PlateNumber { get; set; }
        [Required]
        public  int Kw { get; set; }
        public string DateOfService { get; set; }
        [Required]
        public string ServiceType { get; set; }

    }
}
