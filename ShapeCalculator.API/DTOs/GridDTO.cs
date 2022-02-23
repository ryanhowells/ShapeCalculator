using System.ComponentModel.DataAnnotations;

namespace ShapeCalculator.API.DTOs
{
    public class GridDTO
    {
        [Required]
        [Range(1, 100)]
        public int Height { get; set; }

        [Required]
        [Range(1, 100)]
        public int Width { get; set; }

        [Required]
        [Range(1, 100)]
        public int Size { get; set; }
    }
}
