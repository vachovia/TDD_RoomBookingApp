using Moq;
using Shouldly;
using Microsoft.AspNetCore.Mvc;
using RoomBookingApp.Core.Enums;
using RoomBookingApp.Core.Models;
using RoomBookingApp.Api.Controllers;
using RoomBookingApp.Core.Processors;

namespace RoomBookingApp.Api.Tests
{
    public class RoomBookingControllerTests
    {
        private RoomBookingResult _result;
        private RoomBookingRequest _request;
        private RoomBookingController _controller;
        private Mock<IRoomBookingRequestProcessor> _roomBookingProcessor;

        public RoomBookingControllerTests()
        {
            _roomBookingProcessor = new Mock<IRoomBookingRequestProcessor>();
            _controller = new RoomBookingController(_roomBookingProcessor.Object);
            _request = new RoomBookingRequest();
            _result = new RoomBookingResult();

            _roomBookingProcessor.Setup(x => x.BookRoom(_request)).Returns(_result);
        }

        [Theory]
        [InlineData(1, true, typeof(OkObjectResult), BookingResultFlag.Success)]
        [InlineData(0, false, typeof(BadRequestObjectResult), BookingResultFlag.Failure)]
        public void Should_Call_Booking_Method_When_Valid(int expectedMethodCalls, bool isModelValid, Type expectedActionResultType, BookingResultFlag bookingResultFlag)
        {
            // Arrange
            if (!isModelValid)
            {
                _controller.ModelState.AddModelError("Key", "ErrorMessage");
            }

            _result.Flag = bookingResultFlag;            

            // Act
            var result = _controller.BookRoom(_request);

            // Assert
            result.ShouldBeOfType(expectedActionResultType);
            _roomBookingProcessor.Verify(x => x.BookRoom(_request), Times.Exactly(expectedMethodCalls));
        }
    }
}
