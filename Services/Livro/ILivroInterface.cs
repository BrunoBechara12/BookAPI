using BookAPI.Dto.Autor;
using BookAPI.Dto.Livro;
using BookAPI.Models;

namespace BookAPI.Services.Livro;

public interface ILivroInterface
{
    Task<ResponseModel<List<LivroModel>>> ListarLivros();
    Task<ResponseModel<LivroModel>> BuscarLivroPorId(int idLivro);
    Task<ResponseModel<List<LivroModel>>> BuscarLivrosPorIdAutor(int idAutor);
    Task<ResponseModel<LivroModel>> CriarLivro(LivroCriacaoDto livro);
    Task<ResponseModel<LivroModel>> EditarLivro(LivroEdicaoDto livro);
    Task<ResponseModel<LivroModel>> ExcluirLivro(int idLivro);
}
