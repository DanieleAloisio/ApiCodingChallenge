using ApiCodingChallenge.Models;

namespace ApiCodingChallenge.Interface
{
    public interface IRepository
    {
        Article? Get(Guid id);
        Guid Create(Article article);
        bool Delete(Guid id);
        bool Update(Article articleToUpdate);
    }
}
