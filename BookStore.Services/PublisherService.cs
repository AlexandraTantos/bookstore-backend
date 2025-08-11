using BookStore.Abstraction;
using BookStore.Domain;

namespace BookStore.Services;

public class PublisherService(IPublisherRepository publisherRepository)
{
    public async Task<string> AddPublisherAsync(Publisher publisher, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(publisher.Name))
            throw new ArgumentException("Name is required");

        return await publisherRepository.InsertAsync(publisher, cancellationToken);
    }

    public async Task<List<Publisher>> GetAllPublishersAsync(CancellationToken cancellationToken)
    {
        return await publisherRepository.GetAllAsync(cancellationToken);
    }

    public async Task<Publisher> GetPublisherByIdAsync(string id, CancellationToken cancellationToken)
    {
        return await publisherRepository.GetByIdAsync(id, cancellationToken);
    }
}
