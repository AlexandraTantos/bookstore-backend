using BookStore.Abstraction;
using BookStore.Domain;

namespace BookStore.Services;

public class PublisherService
{
    private readonly IPublisherRepository  _publisherRepository;

    public PublisherService(IPublisherRepository publisherRepository)
    {
        _publisherRepository = publisherRepository;
    }
    
    public async Task<string> AddPublisherAsync(Publisher publisher, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(publisher.Name))
            throw new ArgumentException("Name is required");

        return await _publisherRepository.InsertAsync(publisher, cancellationToken);
    }

    public async Task<List<Publisher>> GetAllPublishersAsync(CancellationToken cancellationToken)
    {
        return await _publisherRepository.GetAllAsync(cancellationToken);
    }

    public async Task<Publisher> GetPublisherByIdAsync(string id, CancellationToken cancellationToken)
    {
        return await _publisherRepository.GetByIdAsync(id, cancellationToken);
    }
}
