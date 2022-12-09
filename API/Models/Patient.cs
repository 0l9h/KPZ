using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Patient
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int DiseaseId { get; set; }
        public Disease? Disease { get; set; }
    }
}
