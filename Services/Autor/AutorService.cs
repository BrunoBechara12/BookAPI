using BookAPI.Data;
using BookAPI.Dto.Autor;
using BookAPI.Dto.Retorno;
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
        try
        {
            var autor = await _context.Autores.FirstOrDefaultAsync(a => a.Id == idAutor);
             
            if (autor != null)
            {
                return ReturnHandler<AutorModel>.Return("Autores encontrados com sucesso!", true, autor);
            }

            return ReturnHandler<AutorModel>.Return("Nenhum autor localizado!", false);
        }
        catch (Exception ex)
        {
            return ReturnHandler<AutorModel>.Return("Erro ao buscar autor!", false, ex);
        }
    }

    public async Task<ResponseModel<AutorModel>> BuscarAutorPorIdLivro(int idLivro)
    {
        try
        {
            var livro = await _context.Livros
                .Include(a => a.Autor)
                .FirstOrDefaultAsync(a => a.Id == idLivro);

            if (livro != null)
            {
                return ReturnHandler<AutorModel>.Return("Autor encontrado com sucesso!", true, livro.Autor);  
            }

            return ReturnHandler<AutorModel>.Return("Nenhum autor localizado!", false);

        }
        catch (Exception ex)
        {
            return ReturnHandler<AutorModel>.Return("Erro ao buscar autor", false, ex);
        }
    }

    public async Task<ResponseModel<AutorModel>> ListarAutores()
    {
        try
        {
            var autores = await _context.Autores.ToListAsync();

            if (autores.Count > 0)
            {
                return ReturnHandler<AutorModel>.Return("Autores encontrados com sucesso!", true, autores);
            }

            return ReturnHandler<AutorModel>.Return("Nenhum autor encontrado!", false);
        }
        catch (Exception ex)
        {
            return ReturnHandler<AutorModel>.Return("Erro ao procurar autores!", false, ex);
        }
    }

    public async Task<ResponseModel<AutorModel>> CriarAutor(AutorCriacaoDto autor)
    {
        try
        {
            var autores = _context.Autores;

            var newAutor = new AutorModel()
            {
                Nome = autor.Nome,
                Sobrenome = autor.Sobrenome
            };

            autores.Add(newAutor);
            await _context.SaveChangesAsync();

            return ReturnHandler<AutorModel>.Return("Autor criado com sucesso!", true, newAutor);

        }
        catch (Exception ex)
        {
            return ReturnHandler<AutorModel>.Return("Erro ao criar autor!", false, ex);
        }
    }

    public async Task<ResponseModel<AutorModel>> EditarAutor(AutorEdicaoDto autor)
    {
        try
        {
            var autorEdicao = _context.Autores.FirstOrDefault(a => a.Id == autor.Id);

            if (autorEdicao != null)
            {
                autorEdicao.Nome = autor.Nome;
                autorEdicao.Sobrenome = autor.Sobrenome;

                _context.Update(autorEdicao);
                await _context.SaveChangesAsync();
  
                return ReturnHandler<AutorModel>.Return("Autor editado com sucesso!", true, autorEdicao);
            }

            return ReturnHandler<AutorModel>.Return("Erro ao encontrar autor para edição!", false);
        }
        catch (Exception ex)
        {
            return ReturnHandler<AutorModel>.Return("Erro ao editar autor!", false, ex);
        }
    }

    public async Task<ResponseModel<AutorModel>> ExcluirAutor(int idAutor)
    {
        try
        {
            var autorRemocao = _context.Autores.FirstOrDefault(a => a.Id == idAutor);

            if (autorRemocao != null)
            {
                _context.Remove(autorRemocao);
                await _context.SaveChangesAsync();

                return ReturnHandler<AutorModel>.Return("Autor excluído com sucesso!", true, autorRemocao);

            }

            return ReturnHandler<AutorModel>.Return("Erro ao encontrar autor para exclusão!", false);
        }
        catch (Exception ex)
        {
            return ReturnHandler<AutorModel>.Return("Erro ao excluir autor!", false, ex);
        }
    }
}
