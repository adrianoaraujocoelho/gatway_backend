using Microsoft.EntityFrameworkCore;
using BackEnd.DataContext;
using BackEnd.Models;
using BackEnd.Service.ProdutoService;
using System;

namespace BackEnd.Service.ProdutoServiceService
{
    public class ProdutoService : IProdutoInterface
    {
        private readonly ApplicationDbContext _context;
        public ProdutoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<List<ProdutoModel>>> CreateProduto(ProdutoModel novoProduto)
        {
            ServiceResponse<List<ProdutoModel>> serviceResponse = new ServiceResponse<List<ProdutoModel>>();

            try
            {
                if (novoProduto == null)
                {
                    serviceResponse.Dados = null;
                    serviceResponse.Mensagem = "Informar dados!";
                    serviceResponse.Sucesso = false;

                    return serviceResponse;
                }

                novoProduto.Data = DateTime.Now.ToLocalTime();
                _context.Add(novoProduto);
                await _context.SaveChangesAsync();

                serviceResponse.Mensagem = "Produto criado com Sucesso!";

            }
            catch (Exception ex)
            {
                serviceResponse.Mensagem = "Erro ao criar o produto. Detalhes: " + ex.Message;
                serviceResponse.Sucesso = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<ProdutoModel>>> DeleteProdutoById(int id)
        {
            ServiceResponse<List<ProdutoModel>> serviceResponse = new ServiceResponse<List<ProdutoModel>>();

            try
            {

#pragma warning disable CS8600 // Conversão de literal nula ou possível valor nulo em tipo não anulável.
                ProdutoModel produto = _context.Produtos.FirstOrDefault(x => x.Id == id);
#pragma warning restore CS8600 // Conversão de literal nula ou possível valor nulo em tipo não anulável.


                if (produto == null)
                {
                    serviceResponse.Dados = null;
                    serviceResponse.Mensagem = "Produto não localizado!";
                    serviceResponse.Sucesso = false;

                    return serviceResponse;
                }


                _context.Produtos.Remove(produto);
                await _context.SaveChangesAsync();


                serviceResponse.Mensagem = "Produto delete com Sucesso!";

            }
            catch (Exception ex)
            {
                serviceResponse.Mensagem = serviceResponse.Mensagem = "Erro ao deletar o produto. Detalhes: " + ex.Message;
                serviceResponse.Sucesso = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<ProdutoModel>> GetProdutoById(int id)
        {
            ServiceResponse<ProdutoModel> serviceResponse = new ServiceResponse<ProdutoModel>();

            try
            {

#pragma warning disable CS8600 // Conversão de literal nula ou possível valor nulo em tipo não anulável.
                ProdutoModel produto = await _context.Produtos.FirstOrDefaultAsync(x => x.Id == id);
#pragma warning restore CS8600 // Conversão de literal nula ou possível valor nulo em tipo não anulável.


                if (produto == null)
                {
                    serviceResponse.Dados = null;
                    serviceResponse.Mensagem = "Produto não localizado!";
                    serviceResponse.Sucesso = false;
                }

                serviceResponse.Dados = produto;

            }
            catch (Exception ex)
            {

                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }

            return serviceResponse;
        }


        public async Task<ServiceResponse<List<ProdutoModel>>> UpdateProduto(ProdutoModel editadoProduto)
        {
            ServiceResponse<List<ProdutoModel>> serviceResponse = new ServiceResponse<List<ProdutoModel>>();

            try

            {

#pragma warning disable CS8600 // Conversão de literal nula ou possível valor nulo em tipo não anulável.
                ProdutoModel produto = await _context.Produtos.FirstOrDefaultAsync(x => x.Id == editadoProduto.Id);
#pragma warning restore CS8600 // Conversão de literal nula ou possível valor nulo em tipo não anulável.


                if (produto == null)
                {
                    serviceResponse.Dados = null;
                    serviceResponse.Mensagem = "Produto não localizado!";
                    serviceResponse.Sucesso = false;

                    return serviceResponse;
                }

                _context.Entry(produto).CurrentValues.SetValues(editadoProduto);
                await _context.SaveChangesAsync();

                serviceResponse.Mensagem = "Produto Editado!";

            }
            catch (Exception ex)
            {
                serviceResponse.Mensagem = "Erro ao Editar o produto. Detalhes: " + ex.Message;
                serviceResponse.Sucesso = false;
            }

            return serviceResponse;
        }


        public async Task<ServiceResponse<ResultadoBuscaProdutosModel>> GetProdutos(RequisicaoBuscaProdutosModel requisicao)
        {
            ServiceResponse<ResultadoBuscaProdutosModel> serviceResponse = new ServiceResponse<ResultadoBuscaProdutosModel>();

            try
            {

                IQueryable<ProdutoModel> produtosQuery = _context.Produtos.AsNoTracking();

                if (!string.IsNullOrEmpty(requisicao.FiltroNome))
                {
                    var filtroNome = requisicao.FiltroNome;
                    produtosQuery = produtosQuery.Where(p => EF.Functions.Like(p.Nome, $"%{filtroNome}%"));
                }

                produtosQuery = produtosQuery.OrderBy(p => p.Id);


                int itensPorPagina = requisicao.Paginacao?.ItensPorPagina ?? 10;
                int paginaAtual = requisicao.Paginacao?.PaginaAtual ?? 1;

                var totalItens = await produtosQuery.CountAsync();

                var paginacao = new ResultadoBuscaPaginadaModel
                {
                    PaginaAtual = paginaAtual,
                    TotalItens = totalItens,
                    TotalPaginas = (int)Math.Ceiling((double)totalItens / itensPorPagina)
                };


                if (paginaAtual > paginacao.TotalPaginas)
                {

                    paginaAtual = paginacao.TotalPaginas;
                }
                var offset = (paginaAtual - 1) * itensPorPagina;
                offset = offset < 0 ? 0 : offset;

                var produtosEncontrados = await produtosQuery
                    .Skip(offset)
                    .Take(itensPorPagina)
                    .ToListAsync();


                var resultadoBusca = new ResultadoBuscaProdutosModel
                {
                    Paginacao = paginacao,
                    Itens = produtosEncontrados
                };

                serviceResponse.Dados = resultadoBusca;
            }
            catch (Exception ex)
            {
                serviceResponse.Mensagem = "Erro ao Buscar produto(s). Detalhes: " + ex.Message;
                serviceResponse.Sucesso = false;
            }

            return serviceResponse;
        }


    }

}




