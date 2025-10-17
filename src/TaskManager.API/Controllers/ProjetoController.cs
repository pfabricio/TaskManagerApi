using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.Dtos;
using TaskManager.Application.Features.Projetos.Command;
using TaskManager.Application.Features.Projetos.Queries;

namespace TaskManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProjetoController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProjetoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{usuarioId}")]
        public async Task<ActionResult<IEnumerable<ProjetoDto>>> GetProjetos(int usuarioId)
        {
            try
            {
                var query = new GetProjetosDoUsuarioQuery(usuarioId);
                var projetos = await _mediator.Send(query);
                return Ok(projetos);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateProjeto([FromBody] CreateProjetoCommand command)
        {
            try
            {
                var resultado = await _mediator.Send(command);
                if (resultado != null)
                    return CreatedAtAction("GetProjetos", new { usuarioId = command.UsuarioId }, command);
                return BadRequest("Não foi possível criar o projeto.");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProjeto(int id)
        {
            try
            {
                var command = new DeleteProjetoCommand {ProjetoId = id};
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
    }
}
