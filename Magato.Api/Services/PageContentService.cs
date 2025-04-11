using Magato.Api.DTO;
using Magato.Api.Models;
using Magato.Api.Validators;
using Magato.Api.Repositories;
using System.Security.Cryptography;
using System.Text;


namespace Magato.Api.Services;

public class PageContentService : IPageContentService
{
    private readonly IPageContentRepository _repo;

    public PageContentService(IPageContentRepository repo)
    {
        _repo = repo;
    }

    public PageContentDto Get(string key)
    {
        var content = _repo.Get(key);
        if (content == null)
            return null; //Fixa null här sen

        return new PageContentDto { Key = content.Key, Value = content.Value };
    }


    public IEnumerable<PageContentDto> GetAll()
    {
        return _repo.GetAll().Select(c => new PageContentDto { Key = c.Key, Value = c.Value });
    }

    public void Update(PageContentDto dto)
    {
        var existing = _repo.Get(dto.Key);
        if (existing == null)
            throw new KeyNotFoundException("Content not found.");

        existing.Value = dto.Value;
        _repo.Update(existing);
    }
    public void Add(PageContentDto dto)
    {
        var entity = new PageContent { Key = dto.Key, Value = dto.Value };
        _repo.Add(entity);
    }


    public void Delete(string key)
    {
        _repo.Delete(key);
    }

}
