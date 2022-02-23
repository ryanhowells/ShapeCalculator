﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using ShapeCalculator.Core.Factories;
using ShapeCalculator.Core.Interfaces;
using ShapeCalculator.Core.Models;
using Xunit;

namespace ShapeCalculator.Core.Tests.Factories
{
    public class ShapeServiceTests
    {
        private readonly Mock<IShapeService> _shapeService = new();

        [Fact]
        public void GivenGridValueIsA1WhenCalculatingCoordinatesThenResultIsValid()
        {
            var expectedResult = new Shape(new List<Coordinate>
            {
                new(0, 0),
                new(0, 10),
                new(10, 10)
            });

            var grid = new Grid(10);
            var gridValue = new GridValue("A1");
            var shapeEnum = ShapeEnum.Triangle;

            _shapeService.Setup(x => x.ProcessLeftSidedTriangle(It.IsAny<Grid>(), It.IsAny<GridValue>())).Returns(expectedResult);
            var shapeFactory = new ShapeFactory(_shapeService.Object);
            var actualResult = shapeFactory.CalculateCoordinates(shapeEnum, grid, gridValue);

            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void GivenGridValueIsA1WhenCalculatingCoordinatesThenProcessLeftSidedTriangleIsCalled()
        {
            var expectedResult = new Shape(new List<Coordinate>
            {
                new(0, 0),
                new(0, 10),
                new(10, 10)
            });

            var grid = new Grid(10);
            var gridValue = new GridValue("A1");
            var shapeEnum = ShapeEnum.Triangle;

            _shapeService.Setup(x => x.ProcessLeftSidedTriangle(It.IsAny<Grid>(), It.IsAny<GridValue>())).Returns(expectedResult);
            var shapeFactory = new ShapeFactory(_shapeService.Object);
            shapeFactory.CalculateCoordinates(shapeEnum, grid, gridValue);

            _shapeService.Verify(x => x.ProcessLeftSidedTriangle(grid, gridValue));
            _shapeService.VerifyNoOtherCalls();
        }

        [Fact]
        public void GivenGridValueIsA2WhenCalculatingCoordinatesThenResultIsValid()
        {
            var expectedResult = new Shape(new List<Coordinate>
            {
                new(0, 0),
                new(10, 10),
                new(10, 10)
            });

            var grid = new Grid(10);
            var gridValue = new GridValue("A2");
            var shapeEnum = ShapeEnum.Triangle;

            _shapeService.Setup(x => x.ProcessRightSidedTriangle(It.IsAny<Grid>(), It.IsAny<GridValue>())).Returns(expectedResult);
            var shapeFactory = new ShapeFactory(_shapeService.Object);
            var actualResult = shapeFactory.CalculateCoordinates(shapeEnum, grid, gridValue);

            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void GivenGridValueIsA2WhenCalculatingCoordinatesThenProcessRightSidedTriangleIsCalled()
        {
            var expectedResult = new Shape(new List<Coordinate>
            {
                new(0, 0),
                new(10, 10),
                new(10, 10)
            });

            var grid = new Grid(10);
            var gridValue = new GridValue("A2");
            var shapeEnum = ShapeEnum.Triangle;

            _shapeService.Setup(x => x.ProcessRightSidedTriangle(It.IsAny<Grid>(), It.IsAny<GridValue>())).Returns(expectedResult);
            var shapeFactory = new ShapeFactory(_shapeService.Object);
            shapeFactory.CalculateCoordinates(shapeEnum, grid, gridValue);

            _shapeService.Verify(x => x.ProcessRightSidedTriangle(grid, gridValue));
            _shapeService.VerifyNoOtherCalls();
        }

        [Fact]
        public void GivenShapeIsNotTriangleWhenCalculatingCoordinatesThenShapeIsNull()
        {
            var grid = new Grid(10);
            var gridValue = new GridValue("A2");
            var shapeEnum = ShapeEnum.Other;

            var shapeFactory = new ShapeFactory(_shapeService.Object);
            var actualResult = shapeFactory.CalculateCoordinates(shapeEnum, grid, gridValue);

            Assert.Null(actualResult);
            _shapeService.VerifyNoOtherCalls();
        }
    }
}