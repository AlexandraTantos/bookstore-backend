using System.Net;
using AutoMapper;
using BookStore.Abstraction;
using BookStore.Domain;
using MediatR;

namespace BookStore.Application.GetAllAuthors;

public class GetAllAuthorsHandler(IAuthorRepository authorRepository, IMapper mapper)
    : IRequestHandler<GetAllAuthorsRequest, GetAllAuthorsResponse>
{
    public async Task<GetAllAuthorsResponse> Handle(GetAllAuthorsRequest request, CancellationToken cancellationToken)
    {
        int skip = (request.Page - 1) * request.PageSize;
        try
        {
            var authors = await authorRepository.GetAllAsync(skip: skip,
                take: request.PageSize,
                sortBy: request.SortBy,
                sortOrder: request.SortOrder,
                firstNameFilter: request.FirstName,
                lastNameFilter: request.LastName,
                birthDateFilter: request.BirthDate,
                cancellationToken);
            var authorDtos = mapper.Map<List<AuthorDto>>(authors);
            
            return new GetAllAuthorsResponse()
            {
                Authors = authorDtos,
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
        catch (Exception ex)
        {
            return new GetAllAuthorsResponse
            {
                StatusCode = System.Net.HttpStatusCode.InternalServerError,
                Message = ex.Message
            };
        }
    }
}