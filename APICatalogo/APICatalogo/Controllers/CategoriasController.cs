using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace APICatalogo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ILogger _logger;
        
        public CategoriasController(AppDbContext context, ILogger<CategoriasController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Categoria>> Get()
        {

            _logger.LogInformation($"===================GET api/categoria===========================");
            try
            {
                return _context.Categorias.AsNoTracking().ToList();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao tentar obter as categorias do banco de dados");
            }
        }

        [HttpGet("Produtos")]
        public ActionResult<IEnumerable<Categoria>> GetCategoriasProdutos()
        {
            _logger.LogInformation($"===================GET api/categorias/produto===========================");

            return _context.Categorias.Include(x => x.Produtos).ToList();
        }

        [HttpGet("{id}", Name = "ObterCategoria")]
        public ActionResult<Categoria> Get(int id)
        {
            try { 
                var categoria = _context.Categorias.AsNoTracking().FirstOrDefault(catg => catg.CategoriaID == id);

                _logger.LogInformation($"===================GET api/categorias/id = {id}===========================");

                if (categoria == null)
                {
                    _logger.LogInformation($"===================GET api/categorias/id = {id} NOT FOUND==============");

                    return NotFound($"A categoria com id = {id} não foi encontrada");
                }
                return categoria;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao tentar obter as categorias do banco de dados");
            }
        }

        [HttpPost]
        public ActionResult Post([FromBody] Categoria categoria)
        {
            try
            {
                _context.Categorias.Add(categoria); //localmente
                _context.SaveChanges(); //faz a inclusão

                //por convenção um metodo post deve retornar um header location, onde coloca a localizão do recurso
                //passa o nome da rota, os parametros da action, no corpo da requisição o produto
                return new CreatedAtRouteResult("ObterCategoria", new { id = categoria.CategoriaID }, categoria);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao tentar criar uma nova categoria");

            }
        }


        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Categoria categoria)
        {
            try
            {
                if (id != categoria.CategoriaID)
                {
                    return BadRequest($"Não foi possível atualizar a categoria com id = {id}");
                }

                _context.Entry(categoria).State = EntityState.Modified; //altera o estado da entitade, faz as alterações 
                _context.SaveChanges(); //persiste no bd
                return Ok($"A categoria com id = {id} foi atulizada com sucesso");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar atualizar a categoria com id = {id}");
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<Categoria> Delete(int id)
        {
            try { 
                var categoria = _context.Categorias.AsNoTracking().FirstOrDefault(catg => catg.CategoriaID == id);

                if (categoria == null)
                {
                    return NotFound($"A categoria com id = {id} não foi encontrada");
                }

                _context.Categorias.Remove(categoria); //apagar a entitade
                _context.SaveChanges(); //persiste no bd
                return Ok($"A categoria com id = {id} foi deletada com sucesso");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar deletar a categoria com id = {id}");
            }
        }
    }
}
