namespace ShapeCalculator.API.DTOs
{
    public class CalculateGridValueDTO
    {
        public GridDTO Grid { get; set; }

        public Vertex TopLeftVertex { get; set; }

        public Vertex OuterVertex { get; set; }

        public Vertex BottomRightVertex { get; set; }

        public int ShapeType { get; set; }
    }

    public class Vertex
    {
        public int x { get; set; }

        public int y { get; set; }
    }
}
