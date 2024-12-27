using AutosportTelemetry.DTO;
using AutosportTelemetry.Models;

namespace AutosportTelemetry.Interfaces;

public interface IUserService
{
    Task<IEnumerable<UserDTO>> GetAllUsersAsync();
    Task<UserDTO> GetUserByIdAsync(int id);
    Task<UserDTO> CreateUserAsync(CreateUserDTO createUserDto);
    Task<UserDTO> UpdateUserAsync(int id, UpdateUserDTO createUserDto);
    Task DeleteUserAsync(int id);
}
