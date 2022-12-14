using ApiCodingChallenge.Dto;
using ApiCodingChallenge.ErrorModel;
using ApiCodingChallenge.Interface;
using ApiCodingChallenge.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiCodingChallenge.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/articles")]
    public class ArticlesController : ControllerBase
    {
        private IRepository _repository;
        public ArticlesController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Article))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ArticleDto> Save([FromBody] ArticleDto article)
        {
            if (article == null)
            {
                return BadRequest(new ErrMsg("empty item data", StatusCodes.Status400BadRequest));
            }
            else if (String.IsNullOrEmpty(article.title))
            {
                return StatusCode(400, new ErrMsg("Title is null.", StatusCodes.Status400BadRequest));
            }

            Article art = new Article(Guid.NewGuid(), article.title, article.text);

            var guid = _repository.Create(art);

            if (guid == Guid.Empty)
            {
                return StatusCode(500, new ErrMsg($"There was a problem saving {art.Id}.", StatusCodes.Status500InternalServerError));
            }

            Response.Headers.Add("Location", $"/api/articles/{art.Id}");
            return Ok(new InfoMsg(art.Id.ToString()));
        }


        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Article))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ArticleDto> Edit(Guid id, [FromBody] ArticleDto article)
        {
            if (article == null)
            {
                return BadRequest(new ErrMsg("empty item data", StatusCodes.Status400BadRequest));
            }
            else if (String.IsNullOrEmpty(article.title))
            {
                return StatusCode(400, new ErrMsg("Title is null.", StatusCodes.Status400BadRequest));
            }

            Article? art = _repository.Get(id);

            if (art == null)
            {
                return StatusCode(404, new ErrMsg("Article not found.", StatusCodes.Status404NotFound));
            }

            art.Title = article.title;
            art.Text = article.text;

            _repository.Update(art);

            return Ok(new InfoMsg("Modified article"));
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(InfoMsg))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrMsg))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrMsg))]

        public IActionResult Delete(Guid id)
        {

            if (id == Guid.Empty)
            {
                return BadRequest(new ErrMsg("Id is null.", StatusCodes.Status400BadRequest));
            }

            Article? articolo = _repository.Get(id);

            if (articolo == null)
            {
                return StatusCode(404, new ErrMsg("Article not found.", StatusCodes.Status404NotFound));
            }

            var response = _repository.Delete(id);

            if (!response)
            {
                return StatusCode(500, new ErrMsg($"Something went wrong.", StatusCodes.Status500InternalServerError));
            }

            return Ok(new InfoMsg($"Deleted article."));
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(InfoMsg))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrMsg))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrMsg))]

        public ActionResult<Article> Get(Guid id)
        {

            if (id == Guid.Empty)
            {
                return BadRequest(new ErrMsg("Id is null.", StatusCodes.Status400BadRequest));
            }

            Article? articolo = _repository.Get(id);

            if (articolo == null)
            {
                return StatusCode(404, new ErrMsg("Article not found.", StatusCodes.Status404NotFound));
            }

            return Ok(articolo);
        }

    }
}