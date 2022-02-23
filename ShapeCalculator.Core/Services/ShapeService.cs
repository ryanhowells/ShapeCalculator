using ShapeCalculator.Core.Interfaces;
using ShapeCalculator.Core.Models;

namespace ShapeCalculator.Core.Services
{
    public class ShapeService : IShapeService
    {
        public Shape ProcessLeftSidedTriangle(Grid grid, GridValue gridValue)
        {
            var leftX = (gridValue.Column - 1) * grid.Size / 2;
            var rightX = leftX + grid.Size;

            var bottomY = gridValue.GetNumericRow() * grid.Size;
            var topLeftY = (gridValue.GetNumericRow() - 1) * grid.Size;

            return new Shape(new List<Coordinate>
            {
                new(leftX, topLeftY),
                new(leftX, bottomY),
                new(rightX, bottomY)
            });
        }

        public Shape ProcessRightSidedTriangle(Grid grid, GridValue gridValue)
        {
            var leftX = (gridValue.Column / 2 - 1) * grid.Size;
            var rightX = leftX + grid.Size;

            var topY = (gridValue.GetNumericRow() - 1) * grid.Size;
            var bottomY = topY + grid.Size;

            return new Shape(new List<Coordinate>
            {
                new(leftX, topY),
                new(rightX, topY),
                new(rightX, bottomY)
            });
        }

        public GridValue ProcessGridValueFromTriangularShape(Grid grid, Triangle triangle)
        {
            var row = triangle.OuterVertex.Y / grid.Size;
            if (triangle.TopLeftVertex.Y == triangle.OuterVertex.Y)
                row++;

            var column = (triangle.OuterVertex.X / grid.Size) * 2;
            if (triangle.TopLeftVertex.X == triangle.OuterVertex.X)
                column++;

            return new GridValue(row, column);
        }
    }
}