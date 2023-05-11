using AutoMapper;
using Library.Model.Models;
using Library.Web.Models;
using LibraryManagementSystem.Models.Account;

namespace Library.Web.Mapping
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "DomainToViewModelMappings"; }
        }

        protected override void Configure()
        {
            Mapper.CreateMap<StaffReader, StaffReaderViewModel>();
            Mapper.CreateMap<User, UserLoginViewModel>();
        }
    }
}
