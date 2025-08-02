using AutoMapper;
using BookStore.Abstraction;
using MediatR;

namespace BookStore.Application.DeletePublisher;

public class DeletePublisherHandler : IRequestHandler<DeletePublisherRequest, DeletePublisherResponse>
{
    private IPublisherRepository publisherRepository;
    private IMapper mapper;

    public DeletePublisherHandler(IPublisherRepository publisherRepository, IMapper mapper)
    {
        this.publisherRepository = publisherRepository;
        this.mapper = mapper;
    }

    public async Task<DeletePublisherResponse> Handle(DeletePublisherRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            await this.publisherRepository.DeleteAsync(request.Id,cancellationToken);
            return new DeletePublisherResponse
            {
                Message = "Publisher successfully deleted",
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
        catch (Exception ex)
        {
            return new DeletePublisherResponse
            {
                Message = "Server error , try again later.",
                StatusCode = System.Net.HttpStatusCode.InternalServerError
            };
        }   
    }
}