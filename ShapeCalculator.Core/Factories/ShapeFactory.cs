using ShapeCalculator.Core.Interfaces;
using ShapeCalculator.Core.Models;

namespace ShapeCalculator.Core.Factories
{
    public class ShapeFactory : IShapeFactory
    {
        private readonly IShapeService _shapeService;

        public ShapeFactory(IShapeService shapeService)
        {
            _shapeService = shapeService;
        }

        public Shape? CalculateCoordinates(ShapeEnum shapeEnum, Grid grid, GridValue gridValue)
        {
            switch (shapeEnum)
            {
                case ShapeEnum.Triangle:
                    var isRightSide = gridValue.Column % 2 == 0;

                    return isRightSide
                        ? _shapeService.ProcessRightSidedTriangle(grid, gridValue)
                        : _shapeService.ProcessLeftSidedTriangle(grid, gridValue);
                default:
                    return null;
            }
        }

        public GridValue? CalculateGridValue(ShapeEnum shapeEnum, Grid grid, Shape shape)
        {
            switch (shapeEnum)
            {
                case ShapeEnum.Triangle:
                    if (shape.Coordinates.Count != 3)
                        return null;

                    var triangle = new Triangle(shape.Coordinates[0], shape.Coordinates[1], shape.Coordinates[2]);

                    return _shapeService.ProcessGridValueFromTriangularShape(grid, triangle);
                default:
                    return null;
            }
        }
    }
}
