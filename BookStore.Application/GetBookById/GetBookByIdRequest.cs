using MediatR;

namespace BookStore.Application.GetBookById;

public class GetBookByIdRequest : IRequest<GetBookByIdResponse>
{
    public string Id {get; set;} = string.Empty;
}