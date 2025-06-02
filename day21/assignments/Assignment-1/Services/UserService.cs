using AutoMapper;
using assignment_1.Interfaces;
using assignment_1.Misc;
using assignment_1.Models;
using assignment_1.Models.DTOs;


namespace assignment_1.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<int, User> _userRepository;
        private readonly IEncryptionService _encryptionService;
        private readonly IMapper _mapper;

        public UserService(IRepository<int, User> userRepository,
                            IEncryptionService encryptionService,
                            IMapper mapper)
        {
            _userRepository = userRepository;
            _encryptionService = encryptionService;
            _mapper = mapper;

        }

        public async Task<User> AddUser(UserAddRequestDTO requestDTO)
        {
            try
            {
                var user = _mapper.Map<UserAddRequestDTO, User>(requestDTO);
                var encryptedData = await _encryptionService.EncryptData(new EncryptModel
                {
                    Data = requestDTO.Password
                });
                user.Password = encryptedData.EncryptedData;
                user.HashKey = encryptedData.HashKey;
                user = await _userRepository.Add(user);
                return user;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public async Task<List<User>> GetAllUsers()
        {
            try
            {
                var users = await _userRepository.GetAll();
                return users.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}