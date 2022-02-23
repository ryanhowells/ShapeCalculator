using System.ComponentModel.DataAnnotations;

namespace ShapeCalculator.API.DTOs
{
    public class CalculateCoordinatesDTO
    {
        [Required]
        [DataType(DataType.Text)]
        public string GridValue { get; set; }

        public GridDTO Grid { get; set; }

        public int ShapeType { get; set; }
    }
}
