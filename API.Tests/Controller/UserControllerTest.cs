using API.Services.Services;
using AutoFixture;
using AutoMapper;
using JewelryStoreAPI.Controllers;
using JewelryStoreAPI.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using Moq.Protected;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace API.Tests.Controller
{
    public class UserControllerTest
    {
        private Mock<IUserService> _userService;
        private Mock<IMapper> _mapper;
        private readonly Mock<IConfiguration> _configuration;
        private readonly Mock<IHttpClientFactory> httpClientFactory;
        private readonly Mock<HttpMessageHandler> mockHttpMessageHandler;

        public UserControllerTest()
        {
            _userService = new Mock<IUserService>();
            _mapper = new Mock<IMapper>();
            _configuration = new Mock<IConfiguration>();
            httpClientFactory = new Mock<IHttpClientFactory>();
            mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        }

        [Fact]
        public void Authenticate_Should_Return_BadRequest()
        {
            var fixture = new Fixture();

            mockHttpMessageHandler.Protected()
                    .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                    .ReturnsAsync(new HttpResponseMessage
                    {
                        StatusCode = HttpStatusCode.OK,
                        Content = new StringContent(fixture.Create<String>()),
                    });

            var client = new HttpClient(mockHttpMessageHandler.Object);
            client.BaseAddress = fixture.Create<Uri>();
            httpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);

            var controller = new UserController(_userService.Object, _mapper.Object, _configuration.Object);
            var result = controller.Authenticate(new UserModel { Username = "WrongUser", Password = "Wrong@Password123" });

            //var test = await new object();
            Assert.NotNull(result);
            // Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsAssignableFrom<BadRequestObjectResult>(result);
        }

        [Fact]
        public void Invalid_API_Request_Should_Return_BadRequest()
        {
            var fixture = new Fixture();

            var client = new HttpClient(mockHttpMessageHandler.Object);
            client.BaseAddress = fixture.Create<Uri>();
            httpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);

            var controller = new UserController(_userService.Object, _mapper.Object, _configuration.Object);
            var result = controller.CalculateAmount(
                new EstimationModel { 
                    Username = "WrongUser",
                    Rate = 25650m,
                    Weight = 14.25m
                    }
                );

            Assert.NotNull(result);
            // Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsAssignableFrom<BadRequestObjectResult>(result);

        }

        #region Service tests
        [Fact]
        public void CalculateFinalAmount_Should_Return_Value()
        {
            _userService.Setup(p => p.CalculateFinalAmount(45225m, 21.25m, 2m))
                .Returns(19220.625m);
        }

        #endregion
    }
}
