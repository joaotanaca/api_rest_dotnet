using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilmeAPI.Data.Dtos;
using FilmesAPI.Data;
using FilmesAPI.Models;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FilmeAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmeController : ControllerBase
    {
        private FilmeContext filmes;

        public FilmeController(FilmeContext context)
        {
            filmes = context;
        }

        [HttpPost]
        public CreatedAtActionResult AdicionarFilme([FromBody] FilmeDTO filmeDTO)
        {
            Filme filme = new Filme
            {
                Titulo = filmeDTO.Titulo,
                Genero = filmeDTO.Genero,
                Duracao = filmeDTO.Duracao,
                Diretor = filmeDTO.Diretor
            };
            filmes.Filmes.Add(filme);
            filmes.SaveChanges();
            return CreatedAtAction(nameof(RecuperarFilme), new { Id = filme.Id }, filme);
        }

        [HttpGet]
        public IEnumerable<Filme> RecuperarFilmes() => filmes.Filmes;

        [HttpGet("{id}")]
        public IActionResult RecuperarFilme(string id)
        {
            Filme filme = filmes.Filmes.FirstOrDefault(filme => filme.Id == new Guid(id));
            if (filme != null)
            {
                return Ok(filme);
            }
            return NotFound();
        }

        [HttpPut("{id}")]
        public IActionResult AtualizaFilme(string id, [FromBody] FilmeDTO body)
        {
            Filme filme = filmes.Filmes.FirstOrDefault(filme => filme.Id == new Guid(id));
            if (filme == null)
            {
                return NotFound();
            }

            filme.Duracao = body.Duracao;
            filme.Diretor = body.Diretor;
            filme.Genero = body.Genero;
            filme.Titulo = body.Titulo;

            filmes.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletaFilme(string id)
        {
            Filme filme = filmes.Filmes.FirstOrDefault(filme => filme.Id == new Guid(id));
            if (filme == null)
            {
                return NotFound();
            }
            filmes.Remove(filme);
            filmes.SaveChanges();
            return NoContent();
        }

    }
}