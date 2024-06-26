using Xunit;
using Moq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Fiap.Api.Alunos.Controllers;
using Fiap.Api.Alunos.Models;
using Fiap.Api.Alunos.Services;

namespace Fiap.Api.Alertas.Test

{
    public class UnitTest1
    {
        public class AlertaControllerTests
        {
            private readonly Mock<IAlertaService> _mockService;
            private readonly IMapper _mapper;
            private readonly AlertaController _controller;

            public AlertaControllerTests()
            {
                _mockService = new Mock<IAlertaService>();

                var config = new MapperConfiguration(cfg => cfg.CreateMap<AlertaModel, AlertaModel>());
                _mapper = config.CreateMapper();

                _controller = new AlertaController(_mockService.Object, _mapper);
            }

            [Fact]
            public void GetAll_ReturnsOkResult_WithListOfAlertas()
            {
                // Arrange
                var alertas = new List<AlertaModel>
            {
                new AlertaModel { Id = 1, Nome = "Alerta 1", Descricao = "Descrição 1" },
                new AlertaModel { Id = 2, Nome = "Alerta 2", Descricao = "Descrição 2" }
            };
                _mockService.Setup(service => service.ListarAlertas()).Returns(alertas);

                // Act
                var result = _controller.GetAll();

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result.Result);
                var returnAlertas = Assert.IsType<List<AlertaModel>>(okResult.Value);
                Assert.Equal(2, returnAlertas.Count);
            }

            [Fact]
            public void GetById_ReturnsNotFoundResult_WhenAlertaDoesNotExist()
            {
                // Arrange
                _mockService.Setup(service => service.ObterAlertaPorId(It.IsAny<int>())).Returns((AlertaModel)null);

                // Act
                var result = _controller.GetById(1);

                // Assert
                Assert.IsType<NotFoundResult>(result.Result);
            }

            [Fact]
            public void GetById_ReturnsOkResult_WithAlerta()
            {
                // Arrange
                var alerta = new AlertaModel { Id = 1, Nome = "Alerta 1", Descricao = "Descrição 1" };
                _mockService.Setup(service => service.ObterAlertaPorId(It.IsAny<int>())).Returns(alerta);

                // Act
                var result = _controller.GetById(1);

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result.Result);
                var returnAlerta = Assert.IsType<AlertaModel>(okResult.Value);
                Assert.Equal(1, returnAlerta.Id);
            }

            [Fact]
            public void Post_ReturnsCreatedAtActionResult()
            {
                // Arrange
                var alerta = new AlertaModel { Id = 1, Nome = "Alerta 1", Descricao = "Descrição 1" };

                // Act
                var result = _controller.Post(alerta);

                // Assert
                var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
                var returnAlerta = Assert.IsType<AlertaModel>(createdAtActionResult.Value);
                Assert.Equal(1, returnAlerta.Id);
            }

            [Fact]
            public void Put_ReturnsNoContentResult()
            {
                // Arrange
                var alerta = new AlertaModel { Id = 1, Nome = "Alerta 1", Descricao = "Descrição 1" };

                _mockService.Setup(service => service.ObterAlertaPorId(It.IsAny<int>())).Returns(alerta);
                _mockService.Setup(service => service.AtualizarAlerta(It.IsAny<AlertaModel>()));

                // Act
                var result = _controller.Put(1, alerta);

                // Assert
                Assert.IsType<NoContentResult>(result);
            }

            [Fact]
            public void Delete_ReturnsNoContentResult()
            {
                // Arrange
                var alerta = new AlertaModel { Id = 1, Nome = "Alerta 1", Descricao = "Descrição 1" };

                _mockService.Setup(service => service.ObterAlertaPorId(It.IsAny<int>())).Returns(alerta);
                _mockService.Setup(service => service.DeletarAlerta(It.IsAny<int>()));

                // Act
                var result = _controller.Delete(1);

                // Assert
                Assert.IsType<NoContentResult>(result);
            }

            [Fact]
            public void Delete_ReturnsNotFoundResult_WhenAlertaDoesNotExist()
            {
                // Arrange
                _mockService.Setup(service => service.ObterAlertaPorId(1)).Returns<AlertaModel>(null);

                // Act
                var result = _controller.Delete(1);

                // Assert
                Assert.IsType<NotFoundResult>(result);
            }
        }
    }
}