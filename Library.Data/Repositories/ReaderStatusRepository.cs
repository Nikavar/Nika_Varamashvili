﻿using Library.Data.Infrastructure;
using Library.Model.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.Repositories
{
    public class ReaderStatusRepository : BaseRepository<ReaderStatus>, IReaderStatusRepository
    {
        public ReaderStatusRepository(DbContext dbContext) : base(dbContext) { }
    }

    public interface IReaderStatusRepository : IBaseRepository<ReaderStatus>
    {

    }
}
