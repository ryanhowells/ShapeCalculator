namespace ShapeCalculator.Core.Models
{
    public class Shape
    {
        public Shape(List<Coordinate> coordinates)
        {
            Coordinates = coordinates;
        }

        protected Shape()
        {
            Coordinates = new List<Coordinate>();
        }

        public List<Coordinate> Coordinates { get; set; }
    }
}
