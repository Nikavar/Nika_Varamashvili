using Library.Model.Models;
using Library.Web.Models;
using Library.Web.Models.Account;
using Library.Web.Models.Book;
using Mapster;

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

            TypeAdapterConfig<Position, PositionViewModel>
               .NewConfig()
               .TwoWays();

            TypeAdapterConfig<Publisher, PublisherViewModel>
               .NewConfig()
               .TwoWays();


			TypeAdapterConfig<Book, CreateBookViewModel>
			   .NewConfig()
			   .TwoWays();

			TypeAdapterConfig<BookPublisher, CreateBookViewModel>
               .NewConfig()
               .TwoWays();
		}
    }
}
