using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.Dtos;
using TaskManager.Application.Features.Comentarios.Command;
using TaskManager.Application.Features.Comentarios.Queries;
using TaskManager.Application.Features.Tarefas.Command;

namespace TaskManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ComentarioController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ComentarioController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{tarefaId}")]
        public async Task<ActionResult<IEnumerable<ComentarioDto>>> GetComentario(int tarefaId)
        {
            try
            {
                var query = new ComentarioDaTarefaQuery(tarefaId);
                var comentarios = await _mediator.Send(query);
                return Ok(comentarios);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateComentario([FromBody] AddComentarioCommand command)
        {
            try
            {
                var resultado = await _mediator.Send(command);
                if (resultado > 0)
                    return CreatedAtAction("GetComentario", new { tarefaId = command.TarefaId }, command);
                return BadRequest("Não foi possível adicionar o comentário.");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
