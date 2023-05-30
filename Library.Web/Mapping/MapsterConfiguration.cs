using AutoMapper;
using DocumentFormat.OpenXml.Spreadsheet;
using Library.Model.Models;
using Library.Web.Models.Account;
using Mapster;
using System.Runtime.CompilerServices;

namespace Library.Web.Mapping
{
    public static class MapsterConfiguration 
    {
       public static void RegisterMaps(this IServiceCollection services)
       {
            TypeAdapterConfig<User, UserLoginViewModel>
                .NewConfig()
                .TwoWays();

            TypeAdapterConfig<User, RegisterViewModel>
                .NewConfig()
                .TwoWays();

            TypeAdapterConfig<StaffReader, RegisterViewModel>
               .NewConfig()
               .TwoWays();
        }
    }
}
