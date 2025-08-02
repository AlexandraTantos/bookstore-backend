using MediatR;

namespace BookStore.Application.UpdatePublisher;

public class UpdatePublisherRequest : IRequest<UpdatePublisherResponse>
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? Address { get; set; }
    public string? Email { get; set; }
}