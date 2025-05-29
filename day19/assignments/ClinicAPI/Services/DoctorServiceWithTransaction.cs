using Microsoft.EntityFrameworkCore;

public class DoctorServiceWithTransaction : IDoctorService
{
    private readonly ClinicContext _clinicContext;
    private readonly DoctorMapper _doctorMapper;
    private readonly SpecialityMapper _specialityMapper;

    public DoctorServiceWithTransaction(ClinicContext clinicContext)
    {
        _clinicContext = clinicContext;
        _doctorMapper = new DoctorMapper();
        _specialityMapper = new();
    }
    public async Task<Doctor> AddDoctor(DoctorAddRequestDTO doctor)
    {
        using var transaction = await _clinicContext.Database.BeginTransactionAsync();
        var newDoctor = _doctorMapper.MapDoctorAddRequestToDoctor(doctor);

        try
        {
            await _clinicContext.AddAsync(newDoctor);
            await _clinicContext.SaveChangesAsync();
            if (doctor.Specialities.Count() > 0)
            {
                var existingSpecialities = await _clinicContext.Specialities.ToListAsync();
                foreach (var item in doctor.Specialities)
                {

                    var speciality = await _clinicContext.Specialities.FirstOrDefaultAsync(s => s.Name.ToLower() == item.Name.ToLower());
                    if (speciality == null)
                    {
                        speciality = _specialityMapper.SpecialityAddRequestToSpeciality(item);
                        await _clinicContext.AddAsync(speciality);
                        await _clinicContext.SaveChangesAsync();
                    }
                    var doctorSpeciality = _specialityMapper.MapDoctorSpecility(newDoctor.Id, speciality.Id);
                    await _clinicContext.AddAsync(doctorSpeciality);

                }
                await _clinicContext.SaveChangesAsync();
                await transaction.CommitAsync();
                return newDoctor;
            }
            else throw new Exception("Doctor should have atleast one speciality");
        }
        catch (Exception exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
        return null;
    }

    public Task<Doctor> GetDoctByName(string name)
    {
        throw new NotImplementedException();
    }

    public Task<ICollection<DoctorsBySpecialityResponseDto>> GetDoctorsBySpeciality(string speciality)
    {
        throw new NotImplementedException();
    }
}