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
        ResponseModel<AutorModel> response = new ResponseModel<AutorModel>();

        try
        {
            var autor = await _context.Autores.FirstOrDefaultAsync(a => a.Id == idAutor);

            if (autor != null)
            {
                return response = new ResponseModel<AutorModel>
                {
                    Mensagem = "Nenhum registro localizado!",
                    Status = false
                };
            }

            return response;
        }
        catch (Exception ex)
        {

            response = new ResponseModel<AutorModel>
            {
                Mensagem = ex.Message,
                Status = false
            };

            return response;
        }
    }

    public async Task<ResponseModel<AutorModel>> BuscarAutorPorIdLivro(int idLivro)
    {
        ResponseModel<AutorModel> response = new ResponseModel<AutorModel>();

        try
        {
            var livro = await _context.Livros
                .Include(a => a.Autor)
                .FirstOrDefaultAsync(a => a.Id == idLivro);

            if (livro == null)
            {
                return response = new ResponseModel<AutorModel>
                {
                    Mensagem = "Nenhum registro localizado!",
                    Status = false
                };
            }

            return response = new ResponseModel<AutorModel>
            {
                Dados = livro.Autor,
                Mensagem = "Autor encontrado com sucesso!",
                Status = true
            };
        }
        catch (Exception ex)
        {

            response = new ResponseModel<AutorModel>
            {
                Mensagem = ex.Message,
                Status = false
            };

            return response;
        }
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
