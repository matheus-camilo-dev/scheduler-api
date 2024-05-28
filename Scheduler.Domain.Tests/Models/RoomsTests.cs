using Moq.AutoMock;
using Scheduler.Domain.Models;

namespace Scheduler.Domain.Tests.Models
{
    public class RoomsTests
    {
        private readonly AutoMocker _autoMocker;

        public RoomsTests()
        {
            _autoMocker = new AutoMocker();
        }

        [Fact]
        public void Constructor_WithTwoParameters_MustBeCreateANewRoomWithStatusFree()
        {
            // Act && Arrange
            var room = new Room(1, "Lab 1");

            // Assert
            Assert.Equal(RoomStatus.Free, room.Status);
        }

        [Theory]
        [InlineData(RoomStatus.Free)]
        [InlineData(RoomStatus.InUse)]
        [InlineData(RoomStatus.InManutention)]
        [InlineData(RoomStatus.Inactive)]
        public void Constructor_WithThreeParameters_MustBeCreateANewRoomWithStatusFree(RoomStatus roomStatus)
        {
            // Act && Arrange
            var room = new Room(1, "Lab 1", roomStatus);

            // Assert
            Assert.Equal(roomStatus, room.Status);
        }

        [Theory]
        [InlineData(RoomStatus.InUse)]
        [InlineData(RoomStatus.InManutention)]
        [InlineData(RoomStatus.Inactive)]
        public void Use_WithStatusDifferentOfFree_MustBeThrowInvalidOperation(RoomStatus roomStatus)
        {
            // Arrange
            var room = new Room(1, "Lab 1", roomStatus);

            // Act
            void action() => room.Use();

            // Assert
            var exception = Assert.Throws<InvalidOperationException>(action);
            Assert.Equal("Just is Possible to use Free Rooms!", exception.Message);
        }

        [Fact]
        public void Use_WithStatusFree_MustBeUpdateStatusToInUse()
        {
            // Arrange
            var room = new Room(1, "Lab 1", RoomStatus.Free);
            var updatedRoom = new Room(1, "Lab 1", RoomStatus.InUse);

            // Act
            room.Use();

            // Assert
            Assert.Equivalent(updatedRoom, room);
        }

        [Theory]
        [InlineData(RoomStatus.InUse)]
        [InlineData(RoomStatus.InManutention)]
        [InlineData(RoomStatus.Inactive)]
        public void Free_WithStatusDifferentOfFree_MustBeUpdateStatusToFree(RoomStatus roomStatus)
        {
            // Arrange
            var room = new Room(1, "Lab 1", roomStatus);
            var updatedRoom = new Room(1, "Lab 1", RoomStatus.Free);

            // Act
            room.Free();

            // Assert
            Assert.Equivalent(updatedRoom, room);            
        }

        [Fact]
        public void Free_WithStatusFree_MustBeThrowInvalidOperationException()
        {
            // Arrange
            var room = new Room(1, "Lab 1", RoomStatus.Free);

            // Act
            void action() => room.Free();

            // Assert
            var exception = Assert.Throws<InvalidOperationException>(action);
            Assert.Equal("Already is free!", exception.Message);
        }
    }
}