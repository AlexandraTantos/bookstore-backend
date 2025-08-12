using BookStore.Abstraction;
using BookStore.Domain;

namespace BookStore.Services;

public class AuthorService(IAuthorRepository authorRepository)
{
    public async Task<string> AddAuthorAsync(Author author, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(author.FirstName) || string.IsNullOrWhiteSpace(author.LastName))
        {
            throw new Exception("First and Last name are required");
        }
        return await authorRepository.InsertAsync(author, cancellationToken);
    }

    public async Task<List<Author>> GetAllAuthorsAsync(CancellationToken cancellationToken)
    {
        return await authorRepository.GetAllAsync(cancellationToken);
    }

    public async Task<Author> GetAuthorAsync(string authorId, CancellationToken cancellationToken)
    {
        return await authorRepository.GetByIdAsync(authorId, cancellationToken);
    }

    public async Task DeleteAuthorByIdAsync(string authorId, CancellationToken cancellationToken)
    {
        await authorRepository.DeleteAsync(authorId, cancellationToken);
    }

    public async Task<bool> UpdateAuthorAsync(Author author, CancellationToken cancellationToken)
    {
        return await authorRepository.UpdateAsync(author, cancellationToken);
    }
}