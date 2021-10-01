using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FilmeAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmeController : ControllerBase
    {
        private static List<Filme> filmes = new List<Filme>();

        [HttpPost]
        public CreatedAtActionResult AdicionarFilme([FromBody] Filme filme)
        {
            Guid guid = Guid.NewGuid();

            filme.Id = guid.ToString();
            filmes.Add(filme);
            return CreatedAtAction(nameof(RecuperarFilme), new { Id = filme.Id }, filme);
        }

        [HttpGet]
        public IActionResult RecuperarFilmes() => Ok(filmes);

        [HttpGet("{id}")]
        public IActionResult RecuperarFilme(string id)
        {
            Filme filme = filmes.FirstOrDefault(filme => filme.Id == id);
            if (filme != null)
            {
                return Ok(filme);
            }
            return NotFound();
        }

    }
}