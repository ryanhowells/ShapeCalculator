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

        /// <summary>
        /// Calculates the Coordinates of a shape given the Grid Value.
        /// </summary>
        /// <param name="calculateCoordinatesRequest"></param>   
        /// <returns>A Coordinates response with a list of coordinates.</returns>
        /// <response code="200">Returns the Coordinates response model.</response>
        /// <response code="400">If an error occurred while calculating the Coordinates.</response>   
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Shape))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("CalculateCoordinates")]
        [HttpPost]
        public IActionResult CalculateCoordinates([FromBody]CalculateCoordinatesDTO calculateCoordinatesRequest)
        {
            var shapeEnum = (ShapeEnum)calculateCoordinatesRequest.ShapeType;
            if (shapeEnum == ShapeEnum.None || shapeEnum == ShapeEnum.Other)
                return BadRequest("Please enter a valid Shape Type.");

            var grid = new Grid(calculateCoordinatesRequest.Grid.Size);
            var gridValue = new GridValue(calculateCoordinatesRequest.GridValue);

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

        /// <summary>
        /// Calculates the Grid Value of a shape given the Coordinates.
        /// </summary>
        /// <remarks>
        /// A Triangle Shape must have 3 vertices, in this order: Top Left Vertex, Outer Vertex, Bottom Right Vertex.
        /// </remarks>
        /// <param name="gridValueRequest"></param>   
        /// <returns>A Grid Value response with a Row and a Column.</returns>
        /// <response code="200">Returns the Grid Value response model.</response>
        /// <response code="400">If an error occurred while calculating the Grid Value.</response>   
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GridValue))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("CalculateGridValue")]
        [HttpPost]
        public IActionResult CalculateGridValue([FromBody]CalculateGridValueDTO gridValueRequest)
        {
            var shapeEnum = (ShapeEnum)gridValueRequest.ShapeType;
            if (shapeEnum == ShapeEnum.None || shapeEnum == ShapeEnum.Other)
                return BadRequest("Please enter a valid Shape Type.");

            var grid = new Grid(gridValueRequest.Grid.Size);
            var shape = new Shape();
            foreach (var vertex in gridValueRequest.Vertices)
            {
                shape.Coordinates.Add(new Coordinate(vertex.x, vertex.y));
            }

            var result = _shapeFactory.CalculateGridValue(shapeEnum, grid, shape);
            if (result == null)
                return BadRequest("An error occurred while calculating grid value.");

            var responseModel = new CalculateGridValueResponseDTO(result.Row!, result.Column);

            return Ok(responseModel);
        }
    }
}
