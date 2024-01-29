using BackEnd.Models;

namespace BackEnd.Service.ProdutoService
{
  public interface IProdutoInterface
  {
    Task<ServiceResponse<ResultadoBuscaProdutosModel>> GetProdutos(RequisicaoBuscaProdutosModel requisicao);
    Task<ServiceResponse<List<ProdutoModel>>> CreateProduto(ProdutoModel novoProduto);
    Task<ServiceResponse<ProdutoModel>> GetProdutoById(int id);
    Task<ServiceResponse<List<ProdutoModel>>> UpdateProduto(ProdutoModel editadoProduto);
    Task<ServiceResponse<List<ProdutoModel>>> DeleteProdutoById(int id);

  }
}