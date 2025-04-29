using AutoMapper;
using nzWalksApi.Models.Domain;
using nzWalksApi.Models.DTO;
namespace nzWalksApi.Mappings
{
    public class AutoMapperProfiles : Profile
    {
public AutoMapperProfiles() { 
        
            CreateMap<Region,RegionDto>().ReverseMap();
            CreateMap<AddRegionDto,Region>().ReverseMap();
            CreateMap<UpdateRegionDto,RegionDto>().ReverseMap();
        
        
        }
    }
}
