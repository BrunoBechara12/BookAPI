using BookAPI.Data;
using BookAPI.Dto.Autor;
using BookAPI.Dto.Livro;
using BookAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BookAPI.Services.Livro;

public class LivroService : ILivroInterface
{
    public readonly AppDbContext _context;
    public LivroService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ResponseModel<LivroModel>> BuscarLivroPorId(int idLivro)
    {

        ResponseModel<LivroModel> response = new ResponseModel<LivroModel>();

        try
        {
            var livro = await _context.Livros
                        .Include(a => a.Autor)
                        .FirstOrDefaultAsync(a => a.Id == idLivro);

            if (livro != null)
            {
                return response = new ResponseModel<LivroModel>
                {
                    Dados = livro,
                    Mensagem = "Livro encontrado com sucesso!",
                    Status = true
                };
            }

            return response = new ResponseModel<LivroModel>
            {
                Mensagem = "Nenhum registro localizado!",
                Status = false
            };
        }
        catch (Exception ex)
        {

            response = new ResponseModel<LivroModel>
            {
                Mensagem = ex.Message,
                Status = false
            };

            return response;
        }
    }

    public async Task<ResponseModel<List<LivroModel>>> BuscarLivrosPorIdAutor(int idAutor)
    {
        ResponseModel<List<LivroModel>> response = new ResponseModel<List<LivroModel>>();

        try
        {
            var livros = await _context.Livros.Include(a => a.Autor).Where(b => b.Autor.Id == idAutor).ToListAsync();

            //var livros = await _context.Livros.Where(a => a.Autor.Id == idAutor).ToListAsync();

            if (livros != null)
            {
                response = new ResponseModel<List<LivroModel>>
                {
                    Dados = livros,
                    Mensagem = "Livros encontrados com sucesso!",
                    Status = true
                };
            }

            return response;
        }
        catch (Exception ex)
        {

            response = new ResponseModel<List<LivroModel>>
            {
                Mensagem = ex.Message,
                Status = false
            };

            return response;
        }
    }

    public async Task<ResponseModel<LivroModel>> CriarLivro(LivroCriacaoDto livroDto)
    {
        ResponseModel<LivroModel> response = new ResponseModel<LivroModel>();

        try
        {
            var autor = await _context.Autores
                    .FirstOrDefaultAsync(a => a.Id == livroDto.AutorId);

            if (autor == null)
            {
                response.Mensagem = "Nenhum registro de autor localizado!";
                return response;
            }

            var livro = new LivroModel()
            {
                Titulo = livroDto.Titulo,
                Autor = autor
            };

            _context.Add(livro);
            await _context.SaveChangesAsync();

            response = new ResponseModel<LivroModel>
            {
                Dados = livro,
                Mensagem = "Livro criado com sucesso!",
                Status = true
            };

            return response;
        }
        catch (Exception ex)
        {

            response = new ResponseModel<LivroModel>
            {
                Mensagem = ex.Message,
                Status = false
            };

            return response;
        }
    }

    public async Task<ResponseModel<LivroModel>> EditarLivro(LivroEdicaoDto livro)
    {
        ResponseModel<LivroModel> response = new ResponseModel<LivroModel>();

        try
        {
            var Autor = await _context.Autores.FirstOrDefaultAsync(a => a.Id == livro.AutorId);

            var livroEditado = _context.Livros.FirstOrDefault(a => a.Id == livro.Id);
                
            if(livroEditado != null)
            {
                livroEditado.Titulo = livro.Titulo;
                livroEditado.Autor = Autor;

                await _context.SaveChangesAsync();
            }

            response = new ResponseModel<LivroModel>
            {
                Dados = livroEditado,
                Mensagem = "Livro editado com sucesso!",
                Status = true
            };

            return response;
        }
        catch (Exception ex)
        {

            response = new ResponseModel<LivroModel>
            {
                Mensagem = ex.Message,
                Status = false
            };

            return response;
        }
    }

    public async Task<ResponseModel<LivroModel>> ExcluirLivro(int idLivro)
    {
        ResponseModel<LivroModel> response = new ResponseModel<LivroModel>();

        try
        {
            var livroRemovido = _context.Livros.Include(a => a.Autor).FirstOrDefault(a => a.Id == idLivro);

            if (livroRemovido != null)
            {

                _context.Remove(livroRemovido);

                await _context.SaveChangesAsync();
            }

            response = new ResponseModel<LivroModel>
            {
                Dados = livroRemovido,
                Mensagem = "Livro removido com sucesso!",
                Status = true
            };

            return response;
        }
        catch (Exception ex)
        {

            response = new ResponseModel<LivroModel>
            {
                Mensagem = ex.Message,
                Status = false
            };

            return response;
        }
    }

    public async Task<ResponseModel<List<LivroModel>>> ListarLivros()
    {
        ResponseModel<List<LivroModel>> response = new ResponseModel<List<LivroModel>>();

        try
        {
            var livros = await _context.Livros
                        .Include(a => a.Autor)
                        .ToListAsync();

            if (livros != null)
            {
                response = new ResponseModel<List<LivroModel>>
                {
                    Dados = livros,
                    Mensagem = "Livros encontrados com sucesso!",
                    Status = true
                };
            }

            return response;
        }
        catch (Exception ex)
        {

            response = new ResponseModel<List<LivroModel>>
            {
                Mensagem = ex.Message,
                Status = false
            };

            return response;
        }
    }
}
