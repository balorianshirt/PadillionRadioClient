using AutoMapper;
using PadillionRadio.Business.Models;
using PadillionRadio.Data.Entities;

namespace PadillionRadio.Business.Configurations
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserIOS, UserModel>()
                .ForMember(x => x.Id, y => y.MapFrom(m => m.Id))
                .ForMember(x => x.Email, y => y.MapFrom(m => m.Email))
                .ForMember(x => x.Code, y => y.MapFrom(m => m.Code))
                .ForMember(x => x.DeviceIdentifier, y => y.MapFrom(m => m.DeviceIdentifier));

            CreateMap<UserModel, UserIOS>()
                .ForMember(x => x.Id, y => y.MapFrom(m => m.Id))
                .ForMember(x => x.Email, y => y.MapFrom(m => m.Email))
                .ForMember(x => x.Code, y => y.MapFrom(m => m.Code))
                .ForMember(x => x.DeviceIdentifier, y => y.MapFrom(m => m.DeviceIdentifier));
        }
    }
}