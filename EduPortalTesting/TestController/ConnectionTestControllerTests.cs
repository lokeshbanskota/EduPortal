using Microsoft.AspNetCore.Mvc;
using Moq;
using Services.Authentication;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduPortalTesting.TestController
{
    public class ConnectionTestControllerTests
    {
        private readonly Mock<IAuthenticateService> _mockAuthenticateService;
        private readonly ConnectionTestController _controller;

        public ConnectionTestControllerTests()
        {
            _mockAuthenticateService = new Mock<IAuthenticateService>();
            _controller = new ConnectionTestController(_mockAuthenticateService.Object);
        }

        [Fact]
        public async Task GetAdmin_ShouldReturnOkWithAdminList()
        {
            // Arrange
            //var mockAdminList = new List<ApplicationUser> { new ApplicationUser { UserName="Admin"},new ApplicationUser {UserName="Lokesh" } };
            var mockAdminList = Task.FromResult((IList<ApplicationUser>)
            [
                 new ApplicationUser { UserName = "Admin1", Email = "admin1@example.com" },
                new ApplicationUser { UserName = "Admin2", Email = "admin2@example.com" }
            ]);
            _mockAuthenticateService
                .Setup(service => service.GetListByUserTypeAsync("Admin"))
                .Returns(mockAdminList);

            // Act
            var result = await _controller.GetAdmin();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);

            // Access the adminList property from the anonymous object
            var response = okResult.Value;
            var adminList = response.GetType().GetProperty("adminList")?.GetValue(response, null);

            Assert.NotNull(adminList);
            Assert.Equal(mockAdminList?.Result, ((Task<IList<ApplicationUser>>)adminList)?.Result);

        }
    }
}
