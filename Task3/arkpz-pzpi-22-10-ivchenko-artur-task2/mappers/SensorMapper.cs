using AutosportTelemetry.DTO;
using AutosportTelemetry.Models;
using AutosportTelemetry.Interfaces;
using AutoMapper;

namespace AutosportTelemetry.Mappers;

public class SensorMapper : Profile
{
    public SensorMapper()
    {
        CreateMap<Sensor, SensorDTO>();
        CreateMap<CreateSensorDTO, Sensor>();
        CreateMap<UpdateSensorDTO, Sensor>();
        CreateMap<RegisterSensorDTO, Sensor>();
    }
}
