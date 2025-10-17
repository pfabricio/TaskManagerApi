using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.Dtos;
using TaskManager.Application.Features.Historicos.Queries;

namespace TaskManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class HistoricosController : ControllerBase
    {
        private readonly IMediator _mediator;
        public HistoricosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{tarefaId}")]
        public async Task<ActionResult<IEnumerable<HistoricoDto>>> GetHistoricoTarefa(int tarefaId)
        {
            try
            {
                var query = new HistoricaPorTarefaQuery(tarefaId);
                var historicos = await _mediator.Send(query);
                return Ok(historicos);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{usuarioId}")]
        public async Task<ActionResult<IEnumerable<HistoricoDto>>> GetHistoricoUsuario(int usuarioId)
        {
            try
            {
                var query = new HistoricoPorUsuarioQuery(usuarioId);
                var historicos = await _mediator.Send(query);
                return Ok(historicos);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
