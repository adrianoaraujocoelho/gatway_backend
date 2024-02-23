using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BackEnd.Models;
using BackEnd.Service.ProdutoService;
using System;


namespace BackEnd.Controllers
{
    [Route("api/produto")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoInterface _produtoInterface;
        public ProdutoController(IProdutoInterface produtoInterface)
        {
            _produtoInterface = produtoInterface;
        }

        [HttpPost("produtos")]
        public async Task<ActionResult<ServiceResponse<ResultadoBuscaProdutosModel>>> GetProdutos(RequisicaoBuscaProdutosModel requisicao)
        {

            ServiceResponse<ResultadoBuscaProdutosModel> serviceResponse = await _produtoInterface.GetProdutos(requisicao);

            return Ok(serviceResponse);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<ProdutoModel>>> GetProdutoById(int id)
        {
            ServiceResponse<ProdutoModel> serviceResponse = await _produtoInterface.GetProdutoById(id);

            return Ok(serviceResponse);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<ProdutoModel>>>> CreateProduto(ProdutoModel novoProduto)
        {
            return Ok(await _produtoInterface.CreateProduto(novoProduto));
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<List<ProdutoModel>>>> UpdateProduto(ProdutoModel editadoProduto)
        {

            ServiceResponse<List<ProdutoModel>> serviceResponse = await _produtoInterface.UpdateProduto(editadoProduto);

            return Ok(serviceResponse);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<ProdutoModel>>> DeleteProdutoById(int id)
        {

            ServiceResponse<List<ProdutoModel>> serviceResponse = await _produtoInterface.DeleteProdutoById(id);

            return Ok(serviceResponse);

        }


    }
}
