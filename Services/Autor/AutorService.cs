using BookAPI.Data;
using BookAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BookAPI.Services.Autor;

public class AutorService : IAutorInterface
{

    public readonly AppDbContext _context;
    public AutorService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ResponseModel<AutorModel>> BuscarAutorPorId(int idAutor)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseModel<AutorModel>> BuscarAutorPorIdLivro(int idLivro)
    {
        throw new NotImplementedException();
    }

    public async Task<ResponseModel<List<AutorModel>>> ListarAutores()
    {
        ResponseModel<List<AutorModel>> response = new ResponseModel<List<AutorModel>>();

        try
        {
            var autores = await _context.Autores.ToListAsync();

            if (autores != null)
            {
                response = new ResponseModel<List<AutorModel>>
                {
                    Dados = autores,
                    Mensagem = "Autores encontrados com sucesso!",
                    Status = true
                };
            }

            return response;
        }
        catch (Exception ex)
        {

            response = new ResponseModel<List<AutorModel>>
            {
                Mensagem = ex.Message,
                Status = false
            };

            return response;

        }
    }
}
