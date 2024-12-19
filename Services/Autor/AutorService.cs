using BookAPI.Data;
using BookAPI.Dto.Autor;
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

    public async Task<ResponseModel<AutorModel>> CriarAutor(AutorCriacaoDto autor)
    {
        ResponseModel<AutorModel> response = new ResponseModel<AutorModel>();

        try
        {
            var autores = _context.Autores;

            var autorBd = new AutorModel()
            {
                Nome = autor.Nome,
                Sobrenome = autor.Sobrenome
            };

            if(autorBd != null)
            {
                autores.Add(autorBd);
                await _context.SaveChangesAsync();
            }

            response = new ResponseModel<AutorModel>
            {
                Dados = autorBd,
                Mensagem = "Autor criado com sucesso!",
                Status = true
            };

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

    public async Task<ResponseModel<AutorModel>> EditarAutor(AutorEdicaoDto autor)
    {
        ResponseModel<AutorModel> response = new ResponseModel<AutorModel>();

        try
        {
            var autorEditado = _context.Autores.FirstOrDefault(a => a.Id == autor.Id);

            if (autorEditado != null)
            {
                autorEditado.Nome = autor.Nome;
                autorEditado.Sobrenome = autor.Sobrenome;

                _context.Update(autorEditado);

                await _context.SaveChangesAsync();
            }

            response = new ResponseModel<AutorModel>
            {
                Dados = autorEditado,
                Mensagem = "Autor editado com sucesso!",
                Status = true
            };

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
    public async Task<ResponseModel<AutorModel>> ExcluirAutor(int idAutor)
    {
        ResponseModel<AutorModel> response = new ResponseModel<AutorModel>();

        try
        {
            var autorRemovido = _context.Autores.FirstOrDefault(a => a.Id == idAutor);

            if (autorRemovido != null)
            {
                _context.Remove(autorRemovido);

                await _context.SaveChangesAsync();
            }

            response = new ResponseModel<AutorModel>
            {
                Dados = autorRemovido,
                Mensagem = "Autor removido com sucesso!",
                Status = true
            };

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
}
