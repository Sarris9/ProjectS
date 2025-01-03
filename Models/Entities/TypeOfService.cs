using System.ComponentModel.DataAnnotations;

namespace ProjectS.Models.Entities
{
    public enum ServiceType { small,medium,large };
    public class TypeOfService
    {
        [Key]
        public int ServiceTypeId { get; set; }
        public ServiceType Type { get; set; }
        public  DateTime AvailableDate { get; set; }

    }
}
