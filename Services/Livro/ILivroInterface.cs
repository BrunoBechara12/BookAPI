using BookAPI.Dto.Autor;
using BookAPI.Models;

namespace BookAPI.Services.Livro;

public interface ILivroInterface
{
    Task<ResponseModel<List<AutorModel>>> ListarLivros();
    Task<ResponseModel<AutorModel>> BuscarLivroPorId(int idAutor);
    Task<ResponseModel<AutorModel>> BuscarLivroPorIdAutor(int idLivro);
    Task<ResponseModel<AutorModel>> CriarLivro(LivroCriacaoDto autor);
    Task<ResponseModel<AutorModel>> EditarLivro(AutorEdicaoDto autor);
    Task<ResponseModel<AutorModel>> ExcluirLivro(int idAutor);
}
