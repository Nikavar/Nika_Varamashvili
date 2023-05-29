using Library.Data.Infrastructure;
using Library.Model.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.Repositories
{
    public class CardRepository : BaseRepository<Card>, ICardRepository
    {
        public CardRepository(IDbFactory dbFactory) : base(dbFactory)
        {
            
        }
    }

    public interface ICardRepository : IBaseRepository<Card>
    {

    }
}
