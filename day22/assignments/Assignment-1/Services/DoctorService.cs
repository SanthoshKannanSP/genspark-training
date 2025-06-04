using System.Threading.Tasks;
using AutoMapper;
using assignment_1.Interfaces;
using assignment_1.Models;
using assignment_1.Models.DTOs;

namespace assignment_1.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IRepository<int, Doctor> _doctorRepository;
        private readonly IRepository<string, User> _userRepository;
        private readonly IEncryptionService _encryptionService;
        private readonly IMapper _mapper;

        public DoctorService(IRepository<int, Doctor> doctorRepository,
                            IRepository<string,User> userRepository,
                            IEncryptionService encryptionService,
                            IMapper mapper)
        {
            _doctorRepository = doctorRepository;
            _userRepository = userRepository;
            _encryptionService = encryptionService;
            _mapper = mapper;

        }

        public async Task<Doctor> AddDoctor(DoctorAddRequestDTO doctor)
        {
            try
            {
                var user = _mapper.Map<DoctorAddRequestDTO, User>(doctor);
                var encryptedData = await _encryptionService.EncryptData(new EncryptModel
                {
                    Data= doctor.Password
                });
                user.Password = encryptedData.EncryptedData;
                user.HashKey = encryptedData.HashKey;
                user.Role = "Doctor";
                user = await _userRepository.Add(user);
                var newDoctor = _mapper.Map<DoctorAddRequestDTO, Doctor>(doctor);
                newDoctor.Status = "Active";
                newDoctor = await _doctorRepository.Add(newDoctor);
                if (newDoctor == null)
                    throw new Exception("Could not add doctor");
                return newDoctor;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            
        }

        public async Task<List<Doctor>> GetAllDoctors()
        {
            try
            {
                var doctors = await _doctorRepository.GetAll();
                return doctors.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}