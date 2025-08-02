using System.Net;
using AutoMapper;
using BookStore.Abstraction;
using BookStore.Domain;
using MediatR;

namespace BookStore.Application.GetPublisherById;

public class GetPublisherByIdHandler : IRequestHandler<GetPublisherByIdRequest, GetPublisherByIdResponse>
{
    private IPublisherRepository publisherRepository;
    private IMapper mapper;

    public GetPublisherByIdHandler(IPublisherRepository publisherRepository, IMapper mapper)
    {
        this.publisherRepository = publisherRepository;
        this.mapper = mapper;
    }

    public async Task<GetPublisherByIdResponse> Handle(GetPublisherByIdRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var publisher = await this.publisherRepository.GetByIdAsync(request.Id,cancellationToken);
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