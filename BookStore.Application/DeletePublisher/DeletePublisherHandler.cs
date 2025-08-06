using AutoMapper;
using BookStore.Abstraction;
using MediatR;

namespace BookStore.Application.DeletePublisher;

public class DeletePublisherHandler(IPublisherRepository publisherRepository)
    : IRequestHandler<DeletePublisherRequest, DeletePublisherResponse>
{
    public async Task<DeletePublisherResponse> Handle(DeletePublisherRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            await publisherRepository.DeleteAsync(request.Id,cancellationToken);
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