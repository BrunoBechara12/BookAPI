using BookAPI.Models;

namespace BookAPI.Dto.Livro;

public class LivroEdicaoDto
{
    public int Id { get; set; }
    public string Titulo { get; set; }
    public int AutorId { get; set; }
}
