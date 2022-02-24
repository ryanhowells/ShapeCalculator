using Microsoft.AspNetCore.Mvc;
using ShapeCalculator.API.DTOs;
using ShapeCalculator.Core;
using ShapeCalculator.Core.Interfaces;
using ShapeCalculator.Core.Models;

namespace ShapeCalculator.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShapeController : ControllerBase
    {
        private readonly IShapeFactory _shapeFactory;

        public ShapeController(IShapeFactory shapeFactory)
        {
            _shapeFactory = shapeFactory;
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Shape))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("CalculateCoordinates")]
        [HttpPost]
        public IActionResult CalculateCoordinates([FromBody]CalculateCoordinatesDTO request)
        {
            var shapeEnum = (ShapeEnum)request.ShapeType;
            if (shapeEnum == 0 || shapeEnum == ShapeEnum.Other)
                return BadRequest("Please enter a valid Shape Type.");

            var grid = new Grid(request.Grid.Size);
            var gridValue = new GridValue(request.GridValue);

            var result = _shapeFactory.CalculateCoordinates(shapeEnum, grid, gridValue);
            if (result == null)
                return BadRequest("An error occurred while calculating coordinates.");

            var responseModel = new CalculateCoordinatesResponseDTO
            {
                Coordinates = new List<CalculateCoordinatesResponseDTO.Coordinate>()
            };

            foreach (var coordinate in result.Coordinates)
            {
                responseModel.Coordinates.Add(
                    new CalculateCoordinatesResponseDTO.Coordinate(coordinate.X, coordinate.Y));
            }

            return Ok(responseModel);
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GridValue))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("CalculateGridValue")]
        [HttpPost]
        public IActionResult CalculateGridValue([FromBody]CalculateGridValueDTO request)
        {
            var shapeEnum = (ShapeEnum)request.ShapeType;
            if (shapeEnum == 0 || shapeEnum == ShapeEnum.Other)
                return BadRequest("Please enter a valid Shape Type.");

            var grid = new Grid(request.Grid.Size);
            var shape = new Shape(new List<Coordinate>
            {
                new(request.TopLeftVertex.x, request.TopLeftVertex.y),
                new(request.OuterVertex.x, request.OuterVertex.y),
                new(request.BottomRightVertex.x, request.BottomRightVertex.y),
            });

            var result = _shapeFactory.CalculateGridValue(shapeEnum, grid, shape);
            if (result == null)
                return BadRequest("An error occurred while calculating grid value.");

            var responseModel = new CalculateGridValueResponseDTO(result.Row!, result.Column);

            return Ok(responseModel);
        }
    }
}
