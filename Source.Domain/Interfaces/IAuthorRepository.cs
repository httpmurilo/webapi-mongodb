using System.Collections.Generic;
using System.Threading.Tasks;
using Source.Domain.Model;

namespace Source.Domain.Interfaces
{
    public interface IAuthorRepository
    {
        Task<IEnumerable<Author>> GetAllAuthors();
        Task AddAuthor(Author author);
        Task<bool> RemoveAuthor(string id);
        Task<bool> UpdateAuthor(string id, Author author);
        Task<Author> GetAuthorById(string id);

    }
}