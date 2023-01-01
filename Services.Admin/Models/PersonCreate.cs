namespace Services.Admin.Models
{
    public class PersonCreate
    {
        public string FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; }
        public string? MiddleName { get; set; }
        public DateTime? Birth { get; set; }
        public DateTime? Death { get; set; }
    }
}
