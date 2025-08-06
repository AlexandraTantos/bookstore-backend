using System.Net;
using AutoMapper;
using BookStore.Abstraction;
using BookStore.Domain;
using MediatR;

namespace BookStore.Application.GetPublisherById;

public class GetPublisherByIdHandler(IPublisherRepository publisherRepository, IMapper mapper)
    : IRequestHandler<GetPublisherByIdRequest, GetPublisherByIdResponse>
{
    public async Task<GetPublisherByIdResponse> Handle(GetPublisherByIdRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var publisher = await publisherRepository.GetByIdAsync(request.Id,cancellationToken);
            var publisherDto = mapper.Map<PublisherDto>(publisher);
            return new GetPublisherByIdResponse
            {
                Publisher = publisherDto,
                StatusCode = HttpStatusCode.OK
            };
        }
        catch (Exception ex)
        {
            return new GetPublisherByIdResponse
            {
                Message = "Server error occurred, please try again later.",
                StatusCode = System.Net.HttpStatusCode.InternalServerError
            };
        }
    }
}