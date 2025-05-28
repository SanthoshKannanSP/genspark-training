using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

public class Appointment
{

    public string AppointmnetNumber { get; set; } = string.Empty;
    public int PatientId { get; set; }
    public int DoctorId { get; set; }
    public DateTime AppointmnetDateTime { get; set; }

    public string Status { get; set; } = string.Empty;
    public Doctor? Doctor { get; set; }
    public Patient? Patient { get; set; }
}