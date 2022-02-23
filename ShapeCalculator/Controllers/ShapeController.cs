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

        [HttpPost(Name = "CalculateCoordinates")]
        public IActionResult CalculateCoordinates(CalculateCoordinatesDTO request)
        {
            if (string.IsNullOrEmpty(request.GridValue) && request.GridValue.Length != 2)
                return BadRequest();

            var grid = new Grid(request.Grid.Size);
            var gridValue = new GridValue(request.GridValue);

            var result = _shapeFactory.CalculateCoordinates((ShapeEnum)request.ShapeType, grid, gridValue);
            if (result == null)
                return BadRequest();

            return Ok(result);
        }

        [HttpPost(Name = "CalculateGridValue")]
        public IActionResult CalculateGridValue(CalculateGridValueDTO request)
        {
            var grid = new Grid(request.Grid.Size);
            var shape = new Shape(new List<Coordinate>
            {
                new(request.AngleVertex.x, request.AngleVertex.y),
                new(request.LeftVertex.x, request.LeftVertex.y),
                new(request.RightVertex.x, request.RightVertex.y),
            });

            var result = _shapeFactory.CalculateGridValue((ShapeEnum)request.ShapeType, grid, shape);
            if (result == null)
                return BadRequest();

            return Ok(result);
        }
    }
}
