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
        [Fact]
        public async Task GetAdmin_ShouldReturnOkWithAdminList_WhenAdminCountIsGreaterThanOne()
        {
            // Arrange
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

            // Access the adminList property from the response object
            var response = okResult.Value;
            var adminList = response.GetType().GetProperty("adminList")?.GetValue(response, null);

            // Assert that the adminList is not null and has more than 1 admin
            Assert.NotNull(adminList);
            var admins = ((Task<IList<ApplicationUser>>)adminList)?.Result.Count;
            Assert.NotNull(admins);  // Ensure the list is of correct type
            Assert.True(admins > 1, "The number of admins should be greater than 1.");
        }


        //[Fact]
        //public async Task GetAdmin_ShouldReturnOkWithNull_WhenServiceReturnsNull()
        //{
        //    // Arrange
        //    var mockAdminList = Task.FromResult<IList<ApplicationUser>>(null); // Null admin list
        //    _mockAuthenticateService
        //        .Setup(service => service.GetListByUserTypeAsync("Admin"))
        //        .Returns(mockAdminList);

        //    // Act
        //    var result = await _controller.GetAdmin();

        //    // Assert
        //    var okResult = Assert.IsType<OkObjectResult>(result);
        //    Assert.NotNull(okResult.Value);

        //    // Access the adminList property from the response object
        //    var response = okResult.Value;
        //    var adminList = response.GetType().GetProperty("adminList")?.GetValue(response, null);

        //    // Assert that adminList is null
        //    Assert.Null(adminList);
        //}
        //[Fact]
        //public async Task GetAdmin_ShouldReturnOkWithEmptyAdminList_WhenNoAdminsFound()
        //{
        //    // Arrange
        //    var mockAdminList = Task.FromResult<IList<ApplicationUser>>(new List<ApplicationUser>());
        //    _mockAuthenticateService
        //        .Setup(service => service.GetListByUserTypeAsync("Admin"))
        //        .Returns(mockAdminList);

        //    // Act
        //    var result = await _controller.GetAdmin();

        //    // Assert
        //    var okResult = Assert.IsType<OkObjectResult>(result);
        //    Assert.NotNull(okResult.Value);

        //    // Access the adminList property from the response object
        //    var response = okResult.Value;
        //    var adminList = response.GetType().GetProperty("adminList")?.GetValue(response, null);

        //    // Assert that adminList is an empty list (no admins found)
        //    Assert.NotNull(adminList);
        //    Assert.Empty((IList<ApplicationUser>)adminList);
        //}

    }
}
