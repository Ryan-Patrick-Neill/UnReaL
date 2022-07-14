using System.ComponentModel.DataAnnotations;

namespace UnReaL.Models
{
    public class ShortURL
    {
        [Required]
        public int Id { get; set; } = 0;
        [Required]
        public string Url { get; set; } = string.Empty;
    }
}
