using System.ComponentModel.DataAnnotations;

namespace AttendanceApi.Models.DTOs;

public class AddSessionRequestDTO : IValidatableObject
{
    public string SessionName { get; set; } = string.Empty;
    public DateOnly Date { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (EndTime <= StartTime)
        {
            yield return new ValidationResult("The EndTime should be after the StartTime", new[] { nameof(StartTime), nameof(EndTime) });
        }

        if (Date <= DateOnly.FromDateTime(DateTime.Now))
        {
            yield return new ValidationResult("The Session Date should be in the future", new[] { nameof(Date) });
        }

        if (SessionName.Length <= 2)
        {
            yield return new ValidationResult("The Session Name should be more than 2 characters");
        }
    }
}