using ApiWeb9.Data;
using ApiWeb9.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiWeb9.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ProdutoController(AppDbContext context) {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<ProdutoModel>> BuscarProdutos( ) {
            var produtos = _context.Produtos.ToList();
            return Ok( produtos );
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<ProdutoModel> BuscarProdutoPorId(int id)
        {
            var produto = _context.Produtos.Find(id);

            if(produto == null)
            {
                return NotFound("Produto não localizado ou inexistenti!");
            }

            return Ok(produto);
        }

        [HttpPost]
        public ActionResult<ProdutoModel> CadastrarProduto(ProdutoModel produtoModel)
        {
            if (produtoModel == null)
            {
                return BadRequest("Ocorreu um erro com a solicitação!");
            }

            _context.Produtos.Add(produtoModel);
            _context.SaveChanges();

            return CreatedAtAction(nameof(BuscarProdutoPorId), new {id = produtoModel.Id}, produtoModel);
        }

        [HttpPut]
        [Route("{id}")]
        public ActionResult<ProdutoModel> EditarProduto(ProdutoModel produtoModel, int id)
        {
            var produto = _context.Produtos.Find(id);

            if (produto == null)
            {
                return NotFound("Produto não localizado ou inexistenti!");
            }

            produto.Nome = produtoModel.Nome;
            produto.Descricao = produtoModel.Descricao;
            produto.QuantidadeEstoque = produtoModel.QuantidadeEstoque;
            produto.CodigoDeBarras = produtoModel.CodigoDeBarras;
            produto.Marca = produtoModel.Marca;

            _context.Produtos.Update(produto);
            _context.SaveChanges();

            return Ok("Dados atualizados com suceso!");

        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult<ProdutoModel> DeletarProduto(int id) {
            var produto = _context.Produtos.Find(id);

            if (produto == null)
            {
                return NotFound("Produto não localizado ou inexistente!");
            }

            _context.Produtos.Remove(produto);
            _context.SaveChanges();

            return  Ok("Produto deletado com suceso!");
        }

    }

}
