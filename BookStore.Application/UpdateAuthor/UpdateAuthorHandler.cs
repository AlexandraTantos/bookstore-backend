using System.Net;
using AutoMapper;
using BookStore.Abstraction;
using BookStore.Application.UpdateBook;
using BookStore.Domain;
using BookStore.Repositories;
using MediatR;

namespace BookStore.Application.UpdateAuthor;

public class UpdateAuthorHandler : IRequestHandler<UpdateAuthorRequest, UpdateAuthorResponse>
{
    private IAuthorRepository authorRepository;
    private IMapper mapper;

    public UpdateAuthorHandler(IAuthorRepository authorRepository, IMapper mapper)
    {
        this.authorRepository = authorRepository;
        this.mapper = mapper;
    }

    public async Task<UpdateAuthorResponse> Handle(UpdateAuthorRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var author = await authorRepository.GetByIdAsync(request.Id, cancellationToken);
            if (author == null)
            {
                return new UpdateAuthorResponse
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Message = "Author not found"
                };
            }

            if (request.FirstName != null) author.FirstName = request.FirstName;
            if (request.LastName != null) author.LastName = request.LastName;
            if (request.BirthDate.HasValue) author.BirthDate = request.BirthDate.Value;
            if (request.SpokenLanguages != null) author.SpokenLanguages = request.SpokenLanguages;
            if (request.Nationality != null) author.Nationality = request.Nationality;

            await authorRepository.UpdateAsync(author, cancellationToken);
            var updatedAuthor = mapper.Map<AuthorDto>(author);
            return new UpdateAuthorResponse
            {
                StatusCode = HttpStatusCode.OK,
                UpdatedAuthor = updatedAuthor,
                Message = "Author updated successfully"
            };
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return new UpdateAuthorResponse
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Message = ex.Message
            };
        }
    }
}