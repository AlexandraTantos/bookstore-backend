using MediatR;

namespace BookStore.Application.GetPublisherById;

public class GetPublisherByIdRequest : IRequest<GetPublisherByIdResponse>
{
    public string Id { get; set; }
}