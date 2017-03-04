﻿using Library.Data.Models;
using Library.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model.Repositories
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        public BookRepository(string connectionString)
            : base(connectionString)
        {
        }
        public IEnumerable<Book> GetAllBooks()
        {
            List<Book> result = new List<Book>();
            using (var command = new SqlCommand(
                @"select b.Id, b.Name, b.IsActive, b.DisplayOrder, b.CreateDate, b.UpdateDate,
                        a.Id, a.FirstName, a.LastName, a.IsActive, a.DisplayOrder, a.CreateDate, a.UpdateDate
                        from Book b inner join BookAuthor ba 
                        on (b.Id = ba.BookId)
                        inner join Author a 
                        on(ba.AuthorId = a.Id)"))
            {
                var books = GetRecords(command);

                foreach (var book in books)
                {
                    var exist = result.FirstOrDefault(x => x.Id == book.Id);
                    if (exist != null)
                    {
                        exist.Authors.AddRange(book.Authors);
                    }
                    else
                    {
                        result.Add(book);
                    }
                }
                return result;
            }
        }
        public IEnumerable<Book> GetAvailableBooks()
        {
            List<Book> result = new List<Book>();
            using (var command = new SqlCommand(
                @"select b.Id, b.Name, b.IsActive, b.DisplayOrder, b.CreateDate, b.UpdateDate,
                        a.Id, a.FirstName, a.LastName, a.IsActive, a.DisplayOrder, a.CreateDate, a.UpdateDate
                        from Book b inner join BookAuthor ba 
                        on (b.Id = ba.BookId)
                        inner join Author a 
                        on(ba.AuthorId = a.Id)
                        where b.Id not in (select BookId from UserBook where CAST(DATEADD(day,BorrowDays,BorrowDate) as DATE) > CAST(GETDATE() as DATE))"))
            {
                var books = GetRecords(command);

                foreach (var book in books)
                {
                    var exist = result.FirstOrDefault(x => x.Id == book.Id);
                    if (exist != null)
                    {
                        exist.Authors.AddRange(book.Authors);
                    }
                    else
                    {
                        result.Add(book);
                    }
                }
                return result;
            }
        }
        public IEnumerable<Book> GetUserBooks(int id)
        {
            // PARAMETERIZED QUERIES!
            List<Book> result = new List<Book>();
            using (var command = new SqlCommand(
                @"select b.Id, b.Name, b.IsActive, b.DisplayOrder, b.CreateDate, b.UpdateDate,
                        a.Id, a.FirstName, a.LastName, a.IsActive, a.DisplayOrder, a.CreateDate, a.UpdateDate
                        from Book b inner join BookAuthor ba 
                        on (b.Id = ba.BookId)
                        inner join Author a 
                        on(ba.AuthorId = a.Id)
                        where b.Id in (select BookId from UserBook where UserId = @id and CAST(DATEADD(day,BorrowDays,BorrowDate) as DATE) > CAST(GETDATE() as DATE))"))
            {
                command.Parameters.Add(new ObjectParameter("id", id));
                var books = GetRecords(command);

                foreach (var book in books)
                {
                    var exist = result.FirstOrDefault(x => x.Id == book.Id);
                    if (exist != null)
                    {
                        exist.Authors.AddRange(book.Authors);
                    }
                    else
                    {
                        result.Add(book);
                    }
                }
                return result;
            }
        }

        public override Book PopulateRecord(SqlDataReader reader)
        {
            List<Author> authors = new List<Author>();
            authors.Add(new Author
            {
                Id = reader.GetInt32(6),
                FirstName = reader.GetString(7),
                LastName = reader.GetString(8),
                IsActive = reader.GetBoolean(9),
                DisplayOrder = reader.GetInt32(10),
                CreateDate = reader.GetDateTime(11),
                UpdateDate = reader.GetDateTime(12),
            });
            return new Book
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                IsActive = reader.GetBoolean(2),
                DisplayOrder = reader.GetInt32(3),
                CreateDate = reader.GetDateTime(4),
                UpdateDate = reader.GetDateTime(5),
                Authors = authors
            };
        }

    }
}