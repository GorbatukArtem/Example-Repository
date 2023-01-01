namespace Services.Admin.Models;

public class PersonalResult
{
    public IEnumerable<PerosnalModel> Personals { get; set; }
}

public class PerosnalModel
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string? LastName { get; set; }
    public string? MiddleName { get; set; }
    public DateTime? Birth { get; set; }
    public DateTime? Death { get; set; }
}
