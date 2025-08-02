using AutoMapper;
using BookStore.Abstraction;
using BookStore.Domain;
using MediatR;

namespace BookStore.Application.GetAllPublishers;

public class GetAllPublishersHandler :IRequestHandler<GetAllPublishersRequest, GetAllPublishersResponse>
{
    private IPublisherRepository publisherRepository;
    private IMapper mapper;

    public GetAllPublishersHandler(IPublisherRepository publisherRepository, IMapper mapper)
    {
        this.publisherRepository = publisherRepository;
        this.mapper = mapper;
    }

    public async Task<GetAllPublishersResponse> Handle(GetAllPublishersRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var publishers = await publisherRepository.GetAllAsync(cancellationToken);
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