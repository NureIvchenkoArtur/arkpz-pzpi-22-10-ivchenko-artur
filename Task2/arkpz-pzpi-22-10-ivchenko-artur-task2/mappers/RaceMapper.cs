using AutoMapper;
using AutosportTelemetry.DTO;
using AutosportTelemetry.Models;

namespace AutosportTelemetry.Mappers;
public class RaceMapper : Profile
{
    public RaceMapper()
    {
        CreateMap<Race, RaceDTO>();
        CreateMap<CreateRaceDTO, Race>();
        CreateMap<RaceDTO, Race>();
    } 
}
