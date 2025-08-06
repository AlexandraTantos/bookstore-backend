using AutoMapper;
using BookStore.Abstraction;
using BookStore.Domain;
using MediatR;

namespace BookStore.Application.GetAllPublishers;

public class GetAllPublishersHandler(IPublisherRepository publisherRepository, IMapper mapper)
    : IRequestHandler<GetAllPublishersRequest, GetAllPublishersResponse>
{
    public async Task<GetAllPublishersResponse> Handle(GetAllPublishersRequest request, CancellationToken cancellationToken)
    {
        try
        {
            int skip = (request.Page - 1) * request.PageSize;
            var publishers = await publisherRepository.GetAllAsync(skip: skip,
                take: request.PageSize,
                sortBy: request.SortBy,
                sortOrder: request.SortOrder,
                nameFilter: request.Name,
                cancellationToken);
            
            var publishersDtos = mapper.Map<List<PublisherDto>>(publishers);
            
            return new GetAllPublishersResponse
            {
                Publishers = publishersDtos,
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
        catch(Exception ex)
        { Console.WriteLine(ex); 
            return new GetAllPublishersResponse
            {
              
                Message = "Server error,try again later",
                StatusCode = System.Net.HttpStatusCode.InternalServerError,
            };
        }
    }
}