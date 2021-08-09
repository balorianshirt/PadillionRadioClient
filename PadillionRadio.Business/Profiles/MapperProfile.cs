using AutoMapper;
using PadillionRadio.Business.Configurations;

namespace PadillionRadio.Business.Profiles
{
    public class MapperProfile
    {
        public static MapperConfiguration InitializeAutoMapper()
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile(new UserProfile());  //mapping between Web and Business layer objects
                });

                return config;
            }
    }
}