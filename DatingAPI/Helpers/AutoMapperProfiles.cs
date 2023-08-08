using AutoMapper;
using DatingAPI.DTOs;
using DatingAPI.Extensions;
using DatingAPI.Models;

namespace DatingAPI.Helpers
{
	public class AutoMapperProfiles : Profile
	{
		public AutoMapperProfiles()
		{
			CreateMap<User, MemberDTO>()
				  .ForMember(dest => dest.PhotoUrl,
					  opt => opt.MapFrom(src => src.Photos.FirstOrDefault(u => u.IsMain == true).Url))
				  .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.DateOfBirth.CalculateAge()));
			CreateMap<Photo, PhotoDto>();
			CreateMap<MemberUpdateDTO, User>();
			CreateMap<RegisterDTO, User>();
		}
	}
}
