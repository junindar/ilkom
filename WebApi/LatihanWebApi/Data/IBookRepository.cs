﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LatihanWebApi.Entity;


namespace LatihanWebApi.Data
{
    public interface IBookRepository
    {
        Task Insert(Book book);
        Task Update(Book book);
        Task Delete(int bookId);
        Task<IEnumerable<Book>> GetAllBooks();
        Task<Book> GetBookById(int bookId);
        Task<IEnumerable<Book>> GetAllBooksByCategoryId(int categoryId);

    }
}
