using AutoMapper;
using BookStore.Abstraction;
using BookStore.Domain;
using MediatR;

namespace BookStore.Application.CreatePublisher;

public class CreatePublisherHandler:IRequestHandler<CreatePublisherRequest, CreatePublisherResponse>
{
    private IPublisherRepository publisherRepository;
    private IMapper mapper;

    public CreatePublisherHandler(IPublisherRepository publisherRepository, IMapper mapper)
    {
        this.publisherRepository = publisherRepository;
        this.mapper = mapper;
    }

    public async Task<CreatePublisherResponse> Handle(CreatePublisherRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            PublisherDto publisherDto = request.PublisherDto;
            Publisher publisher = mapper.Map<Publisher>(publisherDto);
            var response = await this.publisherRepository.InsertAsync(publisher,cancellationToken);
            return new CreatePublisherResponse(response);
        }
        catch (Exception ex)
        {
            return new CreatePublisherResponse
            {
                Message = "Server error, try again later",
                StatusCode = System.Net.HttpStatusCode.InternalServerError
            };
        }
    }
}