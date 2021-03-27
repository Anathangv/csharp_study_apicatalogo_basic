using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Servicos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace APICatalogo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        public ProdutosController( AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Produto>> Get()
        {
            return _context.Produtos.AsNoTracking().ToList();
        }

        //acessar arquivo de configuração
        [HttpGet("Autor")]
        public ActionResult<string> GetAutor()
        {
            var autor = _configuration["autor"];
            return $"Autor {autor}";
        }

        //https://localhost:5001/api/Produtos/testeBind/1/
        [HttpGet("testeBind/{id}")]
        public ActionResult<Produto> Get(int id, [BindRequired] string nome) //nome obrigatorio
        {
            return Ok();
        }

        //Fonte de dados dos parametros
        //https://localhost:5001/api/Produtos/saudacao/Ana/
        [HttpGet("saudacao/{nome}")]
        public ActionResult<string> Get([FromServices] IMeuServico meuservico, string nome)
        {
            return Ok(meuservico.Saudacao(nome));
        }


        [HttpGet("{id:int:min(1)}", Name ="ObterProduto")] //restrição de rotas
        public ActionResult<Produto> Get(int id)
        {
            try
            {
                var produto = _context.Produtos.AsNoTracking().FirstOrDefault(prod => prod.ProdutoId == id);

                if (produto == null)
                {
                    return NotFound($"O produto com o id = {id} não foi encontrado");
                }
                return produto;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar obter o produto  com id = {id} do banco de dados");
            }
        }

        [HttpPost]
        public ActionResult Post([FromBody] Produto produto)
        {
            _context.Produtos.Add(produto); //localmente
            _context.SaveChanges(); //faz a inclusão

            //por convenção um metodo post deve retornar um header location, onde coloca a localizão do recurso
            //passa o nome da rota, os parametros da action, no corpo da requisição o produto
            return new CreatedAtRouteResult("ObterProduto", new { id = produto.ProdutoId }, produto);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Produto produto)
        {
            if(id != produto.ProdutoId)
            {
                return BadRequest();
            }

            _context.Entry(produto).State = EntityState.Modified; //altera o estado da entitade, faz as alterações 
            _context.SaveChanges(); //persiste no bd
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult<Produto> Delete(int id)
        {
            var produto = _context.Produtos.AsNoTracking().FirstOrDefault(prod => prod.ProdutoId == id);

            if(produto == null)
            {
                return NotFound();
            }

            _context.Produtos.Remove(produto); //apagar a entitade
            _context.SaveChanges(); //persiste no bd
            return Ok();
        }
    }
}
