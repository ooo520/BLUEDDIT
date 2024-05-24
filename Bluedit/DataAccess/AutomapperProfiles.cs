using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bluedit.DataAccess
{
    public class AutomapperProfiles : Profile
    {
        public AutomapperProfiles()
        {
            CreateMap<EfModels.Answer, Dbo.Answer>();
            CreateMap<Dbo.Answer, EfModels.Answer>();

            CreateMap<EfModels.Category, Dbo.Category>();
            CreateMap<Dbo.Category, EfModels.Category>();

            CreateMap<EfModels.Opinion, Dbo.Opinion>();
            CreateMap<Dbo.Opinion, EfModels.Opinion>();

            CreateMap<EfModels.Thread, Dbo.Thread>();
            CreateMap<Dbo.Thread, EfModels.Thread>();

            CreateMap<EfModels.User, Dbo.User>();
            CreateMap<Dbo.User, EfModels.User>();
        }
    }
}
