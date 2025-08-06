using System.Net;
using AutoMapper;
using BookStore.Abstraction;
using BookStore.Domain;
using MediatR;

namespace BookStore.Application.UpdateAuthor;

public class UpdateAuthorHandler(IAuthorRepository authorRepository, IMapper mapper)
    : IRequestHandler<UpdateAuthorRequest, UpdateAuthorResponse>
{
    public async Task<UpdateAuthorResponse> Handle(UpdateAuthorRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            if (request.Id == null)
            {
                return new UpdateAuthorResponse
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = "Author ID must be provided"
                };
            }

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