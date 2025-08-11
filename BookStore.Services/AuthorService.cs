using BookStore.Abstraction;
using BookStore.Domain;

namespace BookStore.Services;

public class AuthorService
{
    private readonly IAuthorRepository _authorRepository;

    public AuthorService(IAuthorRepository authorRepository)
    {
        _authorRepository = authorRepository;
    }

    public async Task<string> AddAuthorAsync(Author author, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(author.FirstName) || string.IsNullOrWhiteSpace(author.LastName))
        {
            throw new Exception("First and Last name are required");
        }
        return await _authorRepository.InsertAsync(author, cancellationToken);
    }

    public async Task<List<Author>> GetAllAuthorsAsync(CancellationToken cancellationToken)
    {
        return await _authorRepository.GetAllAsync(cancellationToken);
    }

    public async Task<Author> GetAuthorAsync(string authorId, CancellationToken cancellationToken)
    {
        return await _authorRepository.GetByIdAsync(authorId, cancellationToken);
    }
}