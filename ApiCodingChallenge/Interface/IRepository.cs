using ApiCodingChallenge.Models;

namespace ApiCodingChallenge.Interface
{
    public interface IRepository
    {
        // Returns a found article or null.
        Article? Get(Guid id);
        // Returns the identifier of a created article.
        // Throws an exception if an article is null.
        // Throws an exception if a title is null or empty.
        Guid Create(Article article);
        // Returns true if an article was deleted or false if it was not
        //possible to find it.

        bool Delete(Guid id);
        // Returns true if an article was updated or false if it was not
        //possible to find it.
        // Throws an exception if an articleToUpdate is null.
        // Throws an exception if a title is null or empty.
        bool Update(Article articleToUpdate);
    }
}
