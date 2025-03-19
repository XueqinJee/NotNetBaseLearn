using System.ComponentModel.DataAnnotations;

namespace NotNetBaseLearn.Options;

public class BookOptions
{
    public static readonly string Book = "Book";
    public string? Name { get; set; }

    [Required(ErrorMessage = "Age ²»¿ÉÎª¿Õ£¡")]
    public int? Age { get; set; }
    public string? Author { get; set; }
}