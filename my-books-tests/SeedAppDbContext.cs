namespace my_books_tests
{
    public static class SeedAppDbContext
    {
        internal static void SeedDatabase(AppDbContext context)
        {
            var publishers = new List<Publisher>()
            {
                new Publisher()
                {
                    Id = 1,
                    Name = "Publisher 1"
                },
                new Publisher()
                {
                    Id = 2,
                    Name = "Publisher 2"
                },
                new Publisher()
                {
                    Id = 3,
                    Name = "Publisher 3"
                },
                new Publisher()
                {
                    Id = 4,
                    Name = "Publisher 4"
                },
                new Publisher()
                {
                    Id = 5,
                    Name = "Publisher 5"
                },
                 new Publisher()
                {
                    Id = 6,
                    Name = "Publisher 6"
                }
            };
            context.Publishers.AddRange(publishers);

            var authors = new List<Author>()
            {
                new Author()
                {
                    Id = 1,
                    FullName = "Author 1"
                },
                new Author()
                {
                    Id = 2,
                    FullName = "Author 2"
                }
            };
            context.Authors.AddRange(authors);

            var books = new List<Book>
            {
                new Book()
                {
                    Title = "1st Book Title",
                    Description = "1st Book Description",
                    IsRead = true,
                    DateRead = DateTime.Now.AddDays(-10),
                    Rate = 4,
                    Genre = "Biography",
                    DateAdded = DateTime.Now,
                    PublisherId = 1
                },

                new Book()
                {
                    Title = "2st Book Title",
                    Description = "2st Book Description",
                    IsRead = false,
                    DateRead = DateTime.Now.AddDays(-10),
                    Rate = 4,
                    Genre = "Biography",
                    DateAdded = DateTime.Now,
                    CoverUrl = "https...",
                    PublisherId = 1

        }
            };

            context.Books.AddRange(books);

            var books_authors = new List<Book_Author>()
            {
                new Book_Author()
                {
                    Id = 1,
                    BookId = 1,
                    AuthorId = 1
                },
                new Book_Author()
                {
                    Id = 2,
                    BookId = 1,
                    AuthorId = 2
                },
                new Book_Author()
                {
                    Id = 3,
                    BookId = 2,
                    AuthorId = 2
                }
            };

            context.Book_Authors.AddRange(books_authors);

            context.SaveChanges();
        }
    }
}
