

namespace BackEnd.Models
{

    public class RequisicaoBuscaProdutosModel
    {
        public RequisicaoBuscaPaginadaModel? Paginacao { get; set; }

        public long? FiltroId { get; set; }

        public string? FiltroNome { get; set; }
    }
}

