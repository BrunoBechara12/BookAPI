using BookAPI.Models;
using BookAPI.Services.Autor;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AutorController : ControllerBase
{
    private readonly IAutorInterface _autor;

    public AutorController(IAutorInterface autorInterface)
    {
        _autor = autorInterface;
    }

    [HttpGet("ListarAutores")]
    public async Task<ActionResult<ResponseModel<List<AutorModel>>>> ListarAutores()
    {
        var autores = await _autor.ListarAutores();

        return Ok(autores);
    }

    [HttpGet("BuscarAutorPorIdLivro/{idLivro}")]
    public async Task<ActionResult<ResponseModel<List<AutorModel>>>> BuscarAutorPorIdLivro(int idLivro)
    {
        var autores = await _autor.BuscarAutorPorIdLivro(idLivro);

        return Ok(autores);
    }

    [HttpGet("BuscarAutorPorId/{idAutor}")]
    public async Task<ActionResult<ResponseModel<List<AutorModel>>>> BuscarAutorPorId(int idAutor)
    {
        var autores = await _autor.BuscarAutorPorId(idAutor);

        return Ok(autores);
    }
}
