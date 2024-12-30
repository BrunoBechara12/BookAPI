using BookAPI.Dto.Autor;
using BookAPI.Dto.Livro;
using BookAPI.Models;
using BookAPI.Services.Autor;
using BookAPI.Services.Livro;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class LivroController : ControllerBase
{
    private readonly ILivroInterface _livro;

    public LivroController(ILivroInterface livroInterface)
    {
        _livro = livroInterface;
    }

    [HttpGet("ListarLivros")]
    public async Task<ActionResult<ResponseModel<LivroModel>>> ListarLivros()
    {
        var livros = await _livro.ListarLivros();

        return Ok(livros);
    }

    [HttpGet("BuscarLivrosPorIdAutor/{idAutor}")]
    public async Task<ActionResult<ResponseModel<LivroModel>>> BuscarLivrosPorIdAutor(int idAutor)
    {
        var livros = await _livro.BuscarLivrosPorIdAutor(idAutor);

        return Ok(livros);
    }

    [HttpGet("BuscarLivroPorId/{idLivro}")]
    public async Task<ActionResult<ResponseModel<LivroModel>>> BuscarLivroPorId(int idLivro)
    {
        var livro = await _livro.BuscarLivroPorId(idLivro);

        return Ok(livro);
    }

    [HttpPost("CriarLivro")]
    public async Task<ActionResult<ResponseModel<LivroModel>>> CriarLivro(LivroCriacaoDto livro)
    {
        var Livro = await _livro.CriarLivro(livro);

        return Ok(Livro);
    }

    [HttpPut("EditarLivro")]
    public async Task<ActionResult<ResponseModel<LivroModel>>> EditarLivro(LivroEdicaoDto livro)
    {
        var Livro = await _livro.EditarLivro(livro);

        return Ok(Livro);
    }


    [HttpDelete("ExcluirLivro")]
    public async Task<ActionResult<ResponseModel<LivroModel>>> ExcluirLivro(int idLivro)
    {
        var livro = await _livro.ExcluirLivro(idLivro);

        return Ok(livro);
    }
}
