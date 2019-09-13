using AutoMapper;
using ResultStudioWPF.Domain.DomainModel.Entities;
using ResultStudioWPF.Domain.Interfaces;
using ResultStudioWPF.Models;

namespace ResultStudioWPF.Mappers.Profiles
{
  public class MeasurementMappingProfile : Profile
  {
    public MeasurementMappingProfile()
    {
      CreateMap<IMeasurementPoint, MeasurementPointViewModel>().ReverseMap();
      CreateMap<MeasurementPoint, MeasurementPointViewModel>().ReverseMap();
    }
  }
}
