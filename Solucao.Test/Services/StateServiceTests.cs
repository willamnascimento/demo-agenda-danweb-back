using System.ComponentModel.DataAnnotations;
using System.Net;
using Moq;
using Moq.Protected;
using Solucao.Application.Data;
using Solucao.Application.Data.Entities;
using Solucao.Application.Data.Repositories;
using Solucao.Application.Service.Implementations;

namespace Solucao.Tests
{
    public class StateServiceTests
    {
        private Mock<SolucaoContext> contextMock;
        private Mock<StateRepository> stateRepositoryMock;
        private Mock<HttpMessageHandler> handlerMock;
        private Mock<IHttpClientFactory> mockHttpClientFactory;
        private Mock<HttpClient> httpClientMock;

        public StateServiceTests()
        {
            handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            contextMock = new Mock<SolucaoContext>();
            stateRepositoryMock = new Mock<StateRepository>();

            var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK);

            dynamic json = new[]
            {
                new { id = 1, nome = "Parana", sigla = "PR" },
                new { id = 2, nome = "São Paulo", sigla = "SP" }
            };

            httpResponseMessage.Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(json));
            var validationResult = ValidationResult.Success;

            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(httpResponseMessage)
                .Verifiable();

            httpClientMock = new Mock<HttpClient>(handlerMock.Object);

            mockHttpClientFactory = new Mock<IHttpClientFactory>();

            mockHttpClientFactory.Setup(_ => _.CreateClient(string.Empty)).Returns(httpClientMock.Object);

            stateRepositoryMock.Setup(repo => repo.Add(It.IsAny<State>())).Returns(Task.FromResult<ValidationResult>(validationResult));
        }

        [Fact]
        public async Task AddIBGEStatesList_Success()
        {
            // Arrange

            var stateService = new StateService(stateRepositoryMock.Object, mockHttpClientFactory.Object);

            // Act
            var result = await stateService.AddIBGEStatesList();

            // Assert
            Assert.Equal(ValidationResult.Success, result);
            stateRepositoryMock.Verify(repo => repo.Add(It.IsAny<State>()), Times.Exactly(2));
        }

        [Fact]
        public async Task AddIBGEStatesList_Error()
        {
            // Arrange
            var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.NotFound);

            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(httpResponseMessage)
                .Verifiable();

            var stateService = new StateService(stateRepositoryMock.Object, mockHttpClientFactory.Object);

            // Act
            var result = await stateService.AddIBGEStatesList();

            // Assert
            Assert.Equal(HttpStatusCode.NotFound.ToString(), result.ErrorMessage);
            stateRepositoryMock.Verify(repo => repo.Add(It.IsAny<State>()), Times.Never);
        }
    }
}
