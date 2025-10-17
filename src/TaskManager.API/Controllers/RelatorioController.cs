using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.Dtos;
using TaskManager.Application.Features.Relatorios.Query;

namespace TaskManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RelatorioController : ControllerBase
    {
        private readonly IMediator _mediator;
        public RelatorioController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("relatorio/{usuarioId}")]
        public async Task<ActionResult<RelatorioDesempenhoDto>> GetRelatorioUsuario(int usuarioId)
        {
            try
            {
                var query = new GetRelatorioDesempenhoQuery(usuarioId);
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
