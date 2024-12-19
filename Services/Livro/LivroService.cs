using BookAPI.Dto.Autor;
using BookAPI.Models;

namespace BookAPI.Services.Livro;

public class LivroService : ILivroInterface
{
    public Task<ResponseModel<AutorModel>> BuscarLivroPorId(int idAutor)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseModel<AutorModel>> BuscarLivroPorIdAutor(int idLivro)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseModel<AutorModel>> CriarLivro(LivroCriacaoDto autor)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseModel<AutorModel>> EditarLivro(AutorEdicaoDto autor)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseModel<AutorModel>> ExcluirLivro(int idAutor)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseModel<List<AutorModel>>> ListarLivros()
    {
        throw new NotImplementedException();
    }
}
