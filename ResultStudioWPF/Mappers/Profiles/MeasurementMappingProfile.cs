using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ResultStudioWPF.Application.Interfaces;
using ResultStudioWPF.Domain.DomainModel;
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
    }
  }
}
