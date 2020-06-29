using System.ComponentModel.DataAnnotations;

namespace Core.Filters.Operation
{
    public class OperationFilter
    {
        [Required]
        public decimal ValorInicial { get; set; }

        [Required]
        public int Tempo { get; set; }
    }
}