using Microsoft.EntityFrameworkCore;

public class AppointmentRepository : AbstractRepository<string, Appointment>
{
    protected AppointmentRepository(ClinicContext clinicContext) : base(clinicContext) { }

    public override async Task<Appointment> Get(string key)
    {
        var appointment = await _clinicContext.Appointmnets.SingleOrDefaultAsync(p => p.AppointmnetNumber == key);

        return appointment??throw new Exception("No Appointment with the given ID");
    }

    public override async Task<IEnumerable<Appointment>> GetAll()
    {
        var appointments = _clinicContext.Appointmnets;
        if (appointments.Count() == 0)
            throw new Exception("No Appointments in the database");
        return await appointments.ToListAsync();
    }
}