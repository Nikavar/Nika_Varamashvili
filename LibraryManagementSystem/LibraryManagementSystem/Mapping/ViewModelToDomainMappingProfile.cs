using AutoMapper;
using Library.Model.Models;
using Library.Web.Models;

namespace Library.Web.Mapping
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "ViewModelToDomainMappings"; }
        }

        protected override void Configure()
        {
            Mapper.CreateMap<UserFormViewModel, User>();
        }
    }
}
