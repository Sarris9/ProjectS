using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProjectS.Models
{
    public class UpdateAppointmentDto
    {
        [ForeignKey("TypeOfService")]
        public string ServiceType { get; set; }
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
    }
}
