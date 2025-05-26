public class Patient
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; } = DateTime.Now;

    public int Age => DateTime.Today.Year - DateOfBirth.Year;
}