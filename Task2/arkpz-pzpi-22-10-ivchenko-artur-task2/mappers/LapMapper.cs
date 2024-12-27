using AutosportTelemetry.DTO;
using AutosportTelemetry.Models;
using AutoMapper;

namespace AutosportTelemetry.Mappers;

public class LapMapper : Profile
{
    public LapMapper()
    {
        CreateMap<Lap, LapDTO>();
        CreateMap<CreateLapDTO, Lap>();
        CreateMap<LapDTO, Lap>();
    }
}
