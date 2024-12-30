using BookAPI.Dto.Autor;
using BookAPI.Dto.Retorno;
using BookAPI.Models;

namespace BookAPI.Services.Autor;

public interface IAutorInterface
{
    Task<ResponseModel<AutorModel>> ListarAutores();
    Task<ResponseModel<AutorModel>> BuscarAutorPorId(int idAutor);
    Task<ResponseModel<AutorModel>> BuscarAutorPorIdLivro(int idLivro);
    Task<ResponseModel<AutorModel>> CriarAutor(AutorCriacaoDto autor);
    Task<ResponseModel<AutorModel>> EditarAutor(AutorEdicaoDto autor);
    Task<ResponseModel<AutorModel>> ExcluirAutor(int idAutor);
}
