//using EduPortal.Controllers;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Configuration;
//using Moq;
//using Services.Interfaces;

//namespace EduPortalTesting.Controllers
//{
//    public class AuthenticateControllerTest
//    {
//        private readonly Mock<IAuthenticateService> _mockAuthenticateService;
//        private readonly ConnectionTestController _controller;

//        public AuthenticateControllerTest()
//        {
//            _mockAuthenticateService = new Mock<IAuthenticateService>();
//            var mockConfiguration = new Mock<IConfiguration>();
//            _controller = new ConnectionTestController(_mockAuthenticateService.Object, mockConfiguration.Object);
//        }
//        [Fact]
//        public async Task GetAdmin_ShouldReturnOkWithAdminList()
//        {
//            // Arrange
//            var mockAdminList = new List<string> { "Admin1", "Admin2" };
//            _mockAuthenticateService
//                .Setup(service => service.GetListByUserTypeAsync("Admin"))
//                .ReturnsAsync(mockAdminList);

//            // Act
//            var result = await _controller.;

//            // Assert
//            var okResult = Assert.IsType<OkObjectResult>(result);
//            var response = Assert.IsType<dynamic>(okResult.Value);
//            Assert.Equal(mockAdminList, response.adminList);
//        }
//    }
//}
