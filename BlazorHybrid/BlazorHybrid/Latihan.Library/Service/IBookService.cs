﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Latihan.Library.Entity;

namespace Latihan.Library.Service
{
    public interface IBookService
    {
      Task<List<Book>?> GetBooksAsync();
      Task<Book?> GetBookByIdAsync(int id);

    }
}
