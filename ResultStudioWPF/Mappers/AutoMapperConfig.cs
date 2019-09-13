using System;
using AutoMapper;
using ResultStudioWPF.Application.Interfaces;
using ResultStudioWPF.Domain.DomainModel;
using ResultStudioWPF.Domain.DomainModel.Entities;
using ResultStudioWPF.Domain.Interfaces;

namespace ResultStudioWPF.Mappers
{
  /// <summary>
  /// Singleton for auto mapper
  /// </summary>
  [Obsolete]
  public class AutoMapperConfig
  {
    private static AutoMapperConfig _instance;
    private IMapper _mapper;

    private AutoMapperConfig()
    {
      CreateAutoMapperMaps();
    }

    public static AutoMapperConfig Instance => _instance ?? (_instance = new AutoMapperConfig());
    public IMapper Mapper => _mapper;

    private void CreateAutoMapperMaps()
    {
      var config = new MapperConfiguration(cfg => {
        cfg.CreateMap<IMeasurementPoint, MeasurementPoint>();
      });

      _mapper = config.CreateMapper();
    }
  }
}