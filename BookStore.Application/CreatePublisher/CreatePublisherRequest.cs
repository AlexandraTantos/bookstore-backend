using BookStore.Domain;
using MediatR;

namespace BookStore.Application.CreatePublisher;

public class CreatePublisherRequest:IRequest<CreatePublisherResponse>
{
    public PublisherDto PublisherDto { get; set; } = null!;
}