using AutoMapper;
using BookStore.Abstraction;
using BookStore.Domain;
using MediatR;

namespace BookStore.Application.CreatePublisher;

public class CreatePublisherHandler(IPublisherRepository publisherRepository, IMapper mapper)
    : IRequestHandler<CreatePublisherRequest, CreatePublisherResponse>
{
    public async Task<CreatePublisherResponse> Handle(CreatePublisherRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            PublisherDto publisherDto = request.PublisherDto;
            Publisher publisher = mapper.Map<Publisher>(publisherDto);
            var response = await publisherRepository.InsertAsync(publisher,cancellationToken);
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