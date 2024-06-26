using Asp.Versioning;
using AutoMapper;
using Fiap.Api.Alunos.Data.Context;
using Fiap.Api.Alunos.Models;
using Fiap.Api.Alunos.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Fiap.Api.Alunos.Controllers
{
    [ApiController]
    [Route("api/v{v:apiVersion}/[controller]")]
    [Authorize]
    public class AlertaController : ControllerBase
    {
        private readonly IAlertaService _service;
        private readonly IMapper _mapper;

        public AlertaController(IAlertaService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int id)
        {
            var alertaExistente = _service.ObterAlertaPorId(id);
            if (alertaExistente == null)
                return NotFound();

            _service.DeletarAlerta(id);
            return NoContent();
        }


        [HttpGet]
        [Authorize(Roles = "usuario, admin")]
        public ActionResult <AlertaModel> GetAll()
        {
            var alertas = _service.ListarAlertas();
            return Ok(alertas);
        }

        [HttpGet("paginado")]
        [Authorize(Roles = "usuario, admin")]
        public ActionResult<AlertaModel> GetPaginated([FromQuery] int pagina = 1, [FromQuery] int tamanho = 10)
        {
            var alertas = _service.ListarAlertas(pagina, tamanho);
            return Ok(alertas);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "usuario, admin")]
        public ActionResult<AlertaModel> GetById(int id)
        {
            var alerta = _service.ObterAlertaPorId(id);
            if (alerta == null)
                return NotFound();

            return Ok(alerta);
        }

        [HttpPost]
        [Authorize(Roles = "usuario, admin")]
        public ActionResult Post([FromBody] AlertaModel alerta)
        {
            _service.CriarAlerta(alerta);
            return CreatedAtAction(nameof(GetById), new { id = alerta.Id }, alerta);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public ActionResult Put([FromRoute] int id, [FromBody] AlertaModel alerta)
        {
            _service.AtualizarAlerta(alerta);
            _service.AtualizarAlerta(alerta);

            return NoContent();
        }

    }
}
