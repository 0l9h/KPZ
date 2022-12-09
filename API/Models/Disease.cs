using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Disease
    {
        [Key]
        public int Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
        public int PatientId { get; set; }
        public Patient? Patient { get; set; }
    }
}
