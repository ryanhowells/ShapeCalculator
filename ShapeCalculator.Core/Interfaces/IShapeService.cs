using ShapeCalculator.Core.Models;

namespace ShapeCalculator.Core.Interfaces
{
    public interface IShapeService
    {
        Shape ProcessLeftSidedTriangle(Grid grid, GridValue gridValue);

        Shape ProcessRightSidedTriangle(Grid grid, GridValue gridValue);

        GridValue ProcessGridValueFromTriangularShape(Grid grid, Triangle triangle);
    }
}