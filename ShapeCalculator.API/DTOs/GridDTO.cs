using System.ComponentModel.DataAnnotations;

namespace ShapeCalculator.API.DTOs
{
    public class GridDTO
    {
        [Required]
        [Range(1, 100)]
        public int Size { get; set; }
    }
}
