namespace ShapeCalculator.API.DTOs
{
    public class CalculateGridValueDTO
    {
        public GridDTO Grid { get; set; }

        public Vertex AngleVertex { get; set; }

        public Vertex LeftVertex { get; set; }

        public Vertex RightVertex { get; set; }

        public int ShapeType { get; set; }
    }

    public class Vertex
    {
        public int x { get; set; }

        public int y { get; set; }
    }
}
