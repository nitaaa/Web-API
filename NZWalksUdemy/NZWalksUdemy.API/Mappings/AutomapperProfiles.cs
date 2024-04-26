using AutoMapper;
using NZWalksUdemy.API.Models.Domain;
using NZWalksUdemy.API.Models.DTO;

namespace NZWalksUdemy.API.Mappings
{
    public class AutomapperProfiles : Profile
    {
        public AutomapperProfiles()
        {
            CreateMap<Region,RegionDTO>().ReverseMap();  //.ForMember(x => x.Name, opt => opt.MapFrom(x => x.FullName)).ReverseMap();
            CreateMap<AddRegionRequestDTO, Region>().ReverseMap();  
            CreateMap<UpdateRegionRequestDTO, Region>().ReverseMap();  
            
            CreateMap<AddWalkRequestDTO, Walk>().ReverseMap();
            CreateMap<WalkDTO, Walk>().ReverseMap();

            CreateMap<Difficulty, DiffiultyDTO>().ReverseMap();
        }
    }
}
