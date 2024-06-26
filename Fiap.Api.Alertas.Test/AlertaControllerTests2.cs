using AutoMapper;
using Fiap.Api.Alunos.Controllers;
using Fiap.Api.Alunos.Models;
using Fiap.Api.Alunos.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiap.Api.Alertas.Test
{
    public class AlertaControllerTests
    {
        private readonly Mock<IAlertaService> _mockService;
        private readonly Mock<IMapper> _mockMapper;
        private readonly AlertaController _controller;

        public AlertaControllerTests()
        {
            _mockService = new Mock<IAlertaService>();
            _mockMapper = new Mock<IMapper>();
            _controller = new AlertaController(_mockService.Object, _mockMapper.Object);
        }

        [Fact]
        public void GetAll_ReturnsOkResult_WithListOfAlerts()
        {
            // Arrange
            var alertas = new List<AlertaModel>
        {
            new AlertaModel { Id = 1, Nome = "Alerta 1", Descricao = "Descrição 1", Data = DateTime.Now },
            new AlertaModel { Id = 2, Nome = "Alerta 2", Descricao = "Descrição 2", Data = DateTime.Now }
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
            _mockService.Setup(service => service.ObterAlertaPorId(1)).Returns<AlertaModel>(null);

            // Act
            var result = _controller.GetById(1);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void GetById_ReturnsOkResult_WithAlerta()
        {
            // Arrange
            var alerta = new AlertaModel { Id = 1, Nome = "Alerta 1", Descricao = "Descrição 1", Data = DateTime.Now };
            _mockService.Setup(service => service.ObterAlertaPorId(1)).Returns(alerta);

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
            var alerta = new AlertaModel { Id = 1, Nome = "Alerta 1", Descricao = "Descrição 1", Data = DateTime.Now };
            _mockService.Setup(service => service.CriarAlerta(alerta)).Verifiable();

            // Act
            var result = _controller.Post(alerta);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnAlerta = Assert.IsType<AlertaModel>(createdAtActionResult.Value);
            Assert.Equal(alerta.Id, returnAlerta.Id);
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

        [Fact]
        public void Delete_ReturnsNoContentResult()
        {
            // Arrange
            var alerta = new AlertaModel { Id = 1, Nome = "Alerta 1", Descricao = "Descrição 1", Data = DateTime.Now };
            _mockService.Setup(service => service.ObterAlertaPorId(1)).Returns(alerta);
            _mockService.Setup(service => service.DeletarAlerta(1)).Verifiable();

            // Act
            var result = _controller.Delete(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void Put_ReturnsNoContentResult()
        {
            // Arrange
            var alerta = new AlertaModel { Id = 1, Nome = "Alerta 1", Descricao = "Descrição 1", Data = DateTime.Now };
            _mockService.Setup(service => service.ObterAlertaPorId(1)).Returns(alerta);
            _mockService.Setup(service => service.AtualizarAlerta(alerta)).Verifiable();

            // Act
            var result = _controller.Put(1, alerta);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}
