using Microsoft.AspNetCore.Mvc;
using Moq.AutoMock;
using Scheduler.Domain.Models;
using Scheduler.Controllers;
using Scheduler.Infra;
using Moq;
using Microsoft.EntityFrameworkCore;

namespace Scheduler.Api.Tests.Controllers
{
    public class RoomsControllerTests
    {
        private readonly AutoMocker _autoMocker;

        public RoomsControllerTests()
        {
            _autoMocker = new AutoMocker();
        }

        [Fact(Skip = "Problems to mock ef db context")]
        public void GetAll_WithoutRooms_MustBeReturnEmptyOkResult()
        {
            // Arrange
            var controller = _autoMocker.CreateInstance<RoomsController>();

            // Act
            var result = controller.GetAll() as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<Room>>(result.Value);
            Assert.Empty(result.Value as IEnumerable<Room>);
        }

        [Fact(Skip = "Problems to mock ef db context")]
        public void GetAll_WithRooms_MustBeReturnOkResultRoomList()
        {
            var rooms = new List<Room>
            {
                new Room(1, "Lab 1")
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Room>>();
            mockSet.As<IQueryable<Room>>().Setup(m => m.Provider).Returns(rooms.Provider);
            mockSet.As<IQueryable<Room>>().Setup(m => m.Expression).Returns(rooms.Expression);
            mockSet.As<IQueryable<Room>>().Setup(m => m.ElementType).Returns(rooms.ElementType);
            mockSet.As<IQueryable<Room>>().Setup(m => m.GetEnumerator()).Returns(() => rooms.GetEnumerator());

            var mockContext = new Mock<RoomsContext>();
            mockContext.Setup(c => c.Rooms).Returns(mockSet.Object);

            var controller = new RoomsController(mockContext.Object);

            // Act
            var result = controller.GetAll() as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<Room>>(result.Value);
            Assert.Single(result.Value as IEnumerable<Room>);
            Assert.Equivalent(rooms, result.Value as IEnumerable<Room>);
        }
    }
}