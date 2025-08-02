using MediatR;

namespace BookStore.Application.DeletePublisher;

public class DeletePublisherRequest:IRequest<DeletePublisherResponse>
{
    public string Id { get; set; }
}