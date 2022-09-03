using System.ComponentModel.DataAnnotations;

namespace ApiCodingChallenge.Models
{
    public class Article
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Text { get; set; }

        public Article(Guid id, string title, string text)
        {
            Id = id;
            Title = title;
            Text = text;
        }
    }


}
