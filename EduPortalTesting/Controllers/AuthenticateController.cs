//using EduPortal.Authentication;
//using EduPortal.Models;
//using EduPortal.Services.Interfaces;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Configuration;
//using Moq;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace EduPortalTesting.Controllers
//{
//    public class AuthenticateController
//    {
//        [Fact]
//        public async Task Login_ValidCredentials_ReturnsOk()
//        {
//            // Arrange
//            var mockAuthService = new Mock<IAuthenticateService>();
//            var mockTokenManager = new Mock<TokenManager>();
//            var mockConfiguration = new Mock<IConfiguration>();

//            var loginModel = new LoginModel { Username = "user1", Password = "password123" };
//            var user = new User { Id = 1, Username = "user1" };
//            var roles = new[] { "Admin" };
//            var token = new JwtSecurityToken();

//            mockAuthService.Setup(s => s.CheckUserAsync(loginModel.Username)).ReturnsAsync(user);
//            mockAuthService.Setup(s => s.CheckPasswordAsync(user, loginModel.Password)).ReturnsAsync(true);
//            mockAuthService.Setup(s => s.GetRolesAsync(user)).ReturnsAsync(roles);
//            mockTokenManager.Setup(t => t.CreateToken(user, roles, mockConfiguration.Object)).Returns(token);

//            var controller = new AuthenticateController(mockAuthService.Object, mockTokenManager.Object, mockConfiguration.Object);

//            // Act
//            var result = await controller.Login(loginModel);

//            // Assert
//            var okResult = Assert.IsType<OkObjectResult>(result);
//            var response = Assert.IsType<dynamic>(okResult.Value);
//            Assert.NotNull(response.token);
//            Assert.NotNull(response.expiration);
//        }

//    }
//}
