namespace API.ViewModels
{
    public class PatientViewModel
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
    }
}
