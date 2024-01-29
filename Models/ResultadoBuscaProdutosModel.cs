

namespace BackEnd.Models
{

    public class ResultadoBuscaProdutosModel
    {
        public ResultadoBuscaPaginadaModel? Paginacao { get; set; }

        public List<ProdutoModel>? Itens { get; set; }
    }
}