using assignment_1.Interfaces;
using assignment_1.Models;
using assignment_1.Models.DTOs;
using AutoMapper;

namespace assignment_1.Services
{
    public class PatientService : IPatientService
    {
        private readonly IRepository<int, Patient> _patientRepository;
        private readonly IEncryptionService _encryptionService;
        private readonly IMapper _mapper;

        public PatientService(IRepository<int, Patient> patientRepository,
                            IEncryptionService encryptionService,
                            IMapper mapper)
        {
            _patientRepository = patientRepository;
            _encryptionService = encryptionService;
            _mapper = mapper;

        }
        public async Task<Patient> AddPatient(PatientAddRequestDTO requestDTO)
        {
            try
            {
                var patient = _mapper.Map<PatientAddRequestDTO, Patient>(requestDTO);
                var encryptedData = await _encryptionService.EncryptData(new EncryptModel
                {
                    Data = requestDTO.PhoneNumber
                });
                patient.PhoneNumber = encryptedData.EncryptedData;
                patient.HashKey = encryptedData.HashKey;
                patient = await _patientRepository.Add(patient);
                return patient;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<List<Patient>> GetAllPatients()
        {
            try
            {
                var patients = await _patientRepository.GetAll();
                return patients.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}