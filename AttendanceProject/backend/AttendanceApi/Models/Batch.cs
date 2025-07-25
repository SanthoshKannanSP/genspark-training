namespace AttendanceApi.Models;
public class Batch
{
    public int BatchId { get; set; }
    public string BatchName { get; set; } = string.Empty;

    public List<Student> Students { get; set; } = new();
}
