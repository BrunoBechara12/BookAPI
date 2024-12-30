using BookAPI.Data;
using BookAPI.Dto.Autor;
using BookAPI.Dto.Livro;
using BookAPI.Dto.Retorno;
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
        try
        {
            var livro = await _context.Livros
                        .Include(a => a.Autor)
                        .FirstOrDefaultAsync(a => a.Id == idLivro);

            if (livro != null)
            {
                return ReturnHandler<LivroModel>.Return("Livro encontrado com sucesso!", true, livro);
            }

            return ReturnHandler<LivroModel>.Return("Nenhum livro encontrado com esse ID!", false);
        }
        catch (Exception ex)
        {
            return ReturnHandler<LivroModel>.Return("Erro ao buscar livro!", false, ex);
        }
    }

    public async Task<ResponseModel<LivroModel>> BuscarLivrosPorIdAutor(int idAutor)
    {
        try
        {
            var livros = await _context.Livros.Include(a => a.Autor).Where(b => b.Autor.Id == idAutor).ToListAsync();

            //var livros = await _context.Livros.Where(a => a.Autor.Id == idAutor).ToListAsync();

            if (livros.Count > 0)
            {
                return ReturnHandler<LivroModel>.Return("Livros encontrados com sucesso!", true, livros);
            }

            return ReturnHandler<LivroModel>.Return("Nenhum livro encontrado para esse autor!", false);
        }
        catch (Exception ex)
        {
            return ReturnHandler<LivroModel>.Return("Erro ao buscar livros!", false, ex);
        }
    }

    public async Task<ResponseModel<LivroModel>> CriarLivro(LivroCriacaoDto livroDto)
    {
        try
        {
            var autor = await _context.Autores
                    .FirstOrDefaultAsync(a => a.Id == livroDto.AutorId);

            if (autor == null)
            {
                return ReturnHandler<LivroModel>.Return("Autor não encontrado!", false);
            }

            var livro = new LivroModel()
            {
                Titulo = livroDto.Titulo,
                Autor = autor
            };

            _context.Add(livro);
            await _context.SaveChangesAsync();

            return ReturnHandler<LivroModel>.Return("Livro criado com sucesso!", true, livro);
        }
        catch (Exception ex)
        {
            return ReturnHandler<AutorModel>.Return("Erro ao criar livro!", false, ex);
        }
    }

    public async Task<ResponseModel<LivroModel>> EditarLivro(LivroEdicaoDto livro)
    {
        try
        {
            var Autor = await _context.Autores.FirstOrDefaultAsync(a => a.Id == livro.AutorId);

            var livroEditado = _context.Livros.FirstOrDefault(a => a.Id == livro.Id);
                
            if(livroEditado != null)
            {
                livroEditado.Titulo = livro.Titulo;
                livroEditado.Autor = Autor;

                await _context.SaveChangesAsync();

                return ReturnHandler<LivroModel>.Return("Livro editado com sucesso!", true, livroEditado);
            }

            return ReturnHandler<LivroModel>.Return("Não foi encontrado um livro com esse ID!", false);

        }
        catch (Exception ex)
        {
            return ReturnHandler<LivroModel>.Return("Erro ao editar livro!", false, ex);
        }
    }

    public async Task<ResponseModel<LivroModel>> ExcluirLivro(int idLivro)
    {
        try
        {
            var livroRemovido = _context.Livros.Include(a => a.Autor).FirstOrDefault(a => a.Id == idLivro);

            if (livroRemovido != null)
            {
                _context.Remove(livroRemovido);

                await _context.SaveChangesAsync();

                return ReturnHandler<LivroModel>.Return("Livro excluído com sucesso!", true, livroRemovido);
            }

            return ReturnHandler<LivroModel>.Return("Não foi encontrado um livro com esse ID!", false);
        }
        catch (Exception ex)
        {
            return ReturnHandler<LivroModel>.Return("Erro ao excluir livro!", false, ex);
        }
    }

    public async Task<ResponseModel<LivroModel>> ListarLivros()
    {
        try
        {
            var livros = await _context.Livros
                        .Include(a => a.Autor)
                        .ToListAsync();

            if (livros.Count > 0)
            {
                return ReturnHandler<LivroModel>.Return("Livros encontrados com sucesso!", true, livros);
            }

            return ReturnHandler<LivroModel>.Return("Não foi encontrado nenhum livro!", false);
        }
        catch (Exception ex)
        {
            return ReturnHandler<LivroModel>.Return("Erro ao procurar por livros!", false, ex);
        }
    }
}
