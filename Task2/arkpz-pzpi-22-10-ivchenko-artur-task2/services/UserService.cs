using AutosportTelemetry.DTO;
using AutosportTelemetry.Interfaces;
using AutosportTelemetry.Models;
using AutoMapper;

namespace AutosportTelemetry.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<UserDTO>> GetAllUsersAsync()
    {
        var users = await _userRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<UserDTO>>(users);
    }

    public async Task<UserDTO> GetUserByIdAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        return _mapper.Map<UserDTO>(user);
    }

    public async Task<UserDTO> CreateUserAsync(CreateUserDTO createUserDto)
    {
        var user = _mapper.Map<User>(createUserDto);
        user.RegisteredTime = DateTime.UtcNow;
        await _userRepository.CreateAsync(user);
        return _mapper.Map<UserDTO>(user);
    }

    public async Task<UserDTO> UpdateUserAsync(int id, UpdateUserDTO updateUserDto)
    {
        var existingUser = await _userRepository.GetByIdAsync(id);
        if (existingUser != null)
        {
            _mapper.Map(updateUserDto, existingUser);
            await _userRepository.UpdateAsync(existingUser);
        }
        return _mapper.Map<UserDTO>(existingUser);
    }

    public async Task DeleteUserAsync(int id)
    {
        await _userRepository.DeleteAsync(id);
    }
}
