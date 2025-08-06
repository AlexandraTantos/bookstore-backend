using System.Net;
using AutoMapper;
using BookStore.Abstraction;
using BookStore.Application.DeletePublisher;
using BookStore.Domain;
using MediatR;

namespace BookStore.Application.UpdatePublisher;

public class UpdatePublisherHandler(IPublisherRepository publisherRepository, IMapper mapper)
    : IRequestHandler<UpdatePublisherRequest, UpdatePublisherResponse>
{
    public async Task<UpdatePublisherResponse> Handle(UpdatePublisherRequest request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.Id == null)
            {
                return new UpdatePublisherResponse()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = "Publisher ID must be provided"
                };
            }
            var publisher = await publisherRepository.GetByIdAsync(request.Id,cancellationToken);
                if (publisher == null)
                {
                    return new UpdatePublisherResponse
                    {
                        StatusCode = HttpStatusCode.NotFound,
                        Message = "Publisher not found"
                    };
                }

                if (request.Name != null) publisher.Name = request.Name;
                if (request.Address != null) publisher.Address = request.Address;
                if (request.Email != null) publisher.Email = request.Email;
            
                await publisherRepository.UpdateAsync(publisher, cancellationToken);

                var updatedPublisher = mapper.Map<PublisherDto>(publisher);
                return new UpdatePublisherResponse
                {
                    UpdatedPublisher = updatedPublisher,
                    StatusCode = HttpStatusCode.OK,
                    Message = "Publisher successfully updated"
                };
        }
        catch (Exception ex)
        {
            return new UpdatePublisherResponse
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Message = ex.Message
            };
        }
    }
}