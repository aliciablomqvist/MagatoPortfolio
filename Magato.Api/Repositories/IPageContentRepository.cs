using Magato.Api.Models;

namespace Magato.Api.Repositories;

public interface IPageContentRepository
{
    PageContent? Get(string key);
    IEnumerable<PageContent> GetAll();
    void Add(PageContent content);
    void Update(PageContent content);
    void Delete(string key);

    public PageContent? GetByKey(string key)
    {
        return Get(key);
    }
}
