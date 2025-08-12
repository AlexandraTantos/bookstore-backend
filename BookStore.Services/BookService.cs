using BookStore.Abstraction;
using BookStore.Domain;
using BookStore.Repositories;

namespace BookStore.Services
{
    public class BookService(IBookRepository bookRepository)
    {
        public async Task<string> AddBookAsync(Book book, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(book.Title))
                throw new ArgumentException("Title is required");

            return await bookRepository.InsertAsync(book, cancellationToken);
        }

        public async Task<List<Book>> GetAllBooksAsync(CancellationToken cancellationToken)
        {
            return await bookRepository.GetAllAsync(cancellationToken);
        }

        public async Task<Book> GetBookByIdAsync(string id, CancellationToken cancellationToken)
        {
            return await bookRepository.GetByIdAsync(id, cancellationToken);
        }

        public async Task DeleteBookByIdAsync(string id, CancellationToken cancellationToken)
        {
            await bookRepository.DeleteAsync(id, cancellationToken);
        }

        public async Task<bool> UpdateBookAsync(Book book, CancellationToken cancellationToken)
        {
            return await bookRepository.UpdateAsync(book, cancellationToken);
        }
    }
}