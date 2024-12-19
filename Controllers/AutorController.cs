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

}
