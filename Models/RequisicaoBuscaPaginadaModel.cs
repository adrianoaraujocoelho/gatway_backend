using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BackEnd.Models
{

    public class RequisicaoBuscaPaginadaModel
    {
        public int PaginaAtual { get; set; }

        [Range(10, 100)]
        public int ItensPorPagina { get; set; }
    }


}




