using AutosportTelemetry.DTO;
using AutosportTelemetry.Models;
using AutosportTelemetry.Interfaces;
using AutoMapper;

namespace AutosportTelemetry.Mappers;

public class UserMapper : Profile
{
    public UserMapper()
    {
        CreateMap<User, UserDTO>();
        CreateMap<UpdateUserDTO, User>();
        CreateMap<CreateUserDTO, User>()
            .ForMember(dest => dest.PasswordHash, 
                opt => opt.MapFrom(src => HashPassword(src.Password)));
    }

    private string HashPassword(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
        {
            throw new ArgumentException(nameof(password));
        }

        return BCrypt.Net.BCrypt.HashPassword(password);
    }
}
