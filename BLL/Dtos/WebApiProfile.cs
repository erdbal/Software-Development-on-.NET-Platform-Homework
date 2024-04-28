using AutoMapper;
namespace Szoftverfejlesztés_dotnet_hw.BLL.Dtos
{
    public class WebApiProfile: Profile
    {
        public WebApiProfile()
        {
            CreateMap<DAL.Entities.User, User>();            
            CreateMap<DAL.Entities.Event, Event>();
            CreateMap<DAL.Entities.Group, Group>();
            CreateMap<DAL.Entities.UserGroup, UserGroup>();
            CreateMap<DAL.Entities.Comment, Comment>();
        }
    }
}
