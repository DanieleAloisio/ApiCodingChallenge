using ApiCodingChallenge.Interface;
using ApiCodingChallenge.Models;

namespace ApiCodingChallenge.Services
{
    public class ArticleRepository : IRepository
    {

        private ApiDbContext _apiDbContext;

        public ArticleRepository(ApiDbContext apiDbContext)
        {
            _apiDbContext = apiDbContext;
        }

        public Guid Create(Article article)
        {
            _apiDbContext.Articles.Add(article);
            _apiDbContext.SaveChanges();
            return article.Id;
        }

        public bool Delete(Guid id)
        {
            Article article = Get(id);
            _apiDbContext.Articles.Remove(article);
            return Salva();

        }

        public Article? Get(Guid id)
        {
            return _apiDbContext.Articles.FirstOrDefault(x => x.Id == id);
        }

        public bool Update(Article articleToUpdate)
        {
            _apiDbContext.Articles.Update(articleToUpdate);
            return Salva();
        }
        private bool Salva()
        {
            var saved = _apiDbContext.SaveChanges();
            return saved >= 0 ? true : false;

        }
    }
}
