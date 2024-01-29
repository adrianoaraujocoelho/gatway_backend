using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace BackEnd.Models
{
    public class ProdutoModel
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        [StringLength(40)]
        public required string Nome { get; set; }

        [Required]
        [Column(TypeName = "decimal(8,2)")]
        public decimal Preco { get; set; }

        public DateTime Data { get; set; } = DateTime.Now.ToLocalTime();
    }

}

