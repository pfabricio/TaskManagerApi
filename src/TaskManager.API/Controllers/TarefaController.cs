using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.Dtos;
using TaskManager.Application.Features.Tarefas.Command;
using TaskManager.Application.Features.Tarefas.Queries;

namespace TaskManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TarefaController : ControllerBase
    {
        private readonly IMediator _mediator;
        public TarefaController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{projetoId}")]
        public async Task<ActionResult<IEnumerable<TarefaDto>>> GetTarefasPorProjeto(int projetoId)
        {
            try
            {
                var query = new GetTarefasPorProjetoQuery(projetoId);
                var tarefas = await _mediator.Send(query);
                return Ok(tarefas);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateTarefa([FromBody] CreateTarefaCommand command)
        {
            try
            {
                var resultado = await _mediator.Send(command);
                if (resultado != null)
                    return CreatedAtAction("GetTarefasPorProjeto", new { projetoId = command.ProjetoId }, command);
                return BadRequest("Não foi possível criar a tarefa.");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTarefa(int id, [FromBody] UpdateTarefaCommand command)
        {
            try
            {
                command.TarefaId = id;
                var resultado = await _mediator.Send(command);
                if (resultado)
                    return NoContent();
                return NotFound("Tarefa não encontrada.");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTarefa(int id)
        {
            try
            {
                var command = new DeleteTarefaCommand(id);
                var resultado = await _mediator.Send(command);
                if (resultado != null)
                    return NoContent();
                return NotFound("Tarefa não encontrada.");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
