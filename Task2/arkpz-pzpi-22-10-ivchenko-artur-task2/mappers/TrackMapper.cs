using AutoMapper;
using AutosportTelemetry.DTO;
using AutosportTelemetry.Models;

namespace AutosportTelemetry.Mappers;

public class TrackMapper : Profile
{
    public TrackMapper()
    {
        CreateMap<Track, TrackDTO>();
        CreateMap<CreateTrackDTO, Track>();
    }
}
