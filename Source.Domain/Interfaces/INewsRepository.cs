using System.Collections.Generic;
using System.Threading.Tasks;
using Source.Domain.Model;

namespace Source.Domain.Interfaces
{
    public interface INewsRepository
    {
        Task<IEnumerable<News>> GetAllNews();
        Task AddNews(News news);
        Task<bool> RemoveNews(string id);
        Task<bool> UpdateNews(string id, News news);
        Task<IEnumerable<News>> GetNews(string parameter);
        Task<News> GetNewsForId(string id);

    }
}