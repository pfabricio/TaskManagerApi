using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.Dtos;
using TaskManager.Application.Features.Usuario.Command;
using TaskManager.Application.Features.Usuario.Queries;
using TaskManager.Domain.Entities;

namespace TaskManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsuarioController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UsuarioController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<Usuario>>> GetUsuarios()
        {
            try
            {
                var query = new GetAllUsuariosQuery();
                var usuarios = await _mediator.Send(query);
                return Ok(usuarios);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{usuarioId}")]
        public async Task<ActionResult<List<Usuario>>> GetUsuarios(int usuarioId)
        {
            try
            {
                var query = new GetUsuarioByIdQuery(usuarioId);
                var usuario = await _mediator.Send(query);
                return Ok(usuario);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("registrar")]
        public async Task<ActionResult<UsuarioDto>> RegistrarUsuario([FromBody] AddUsuarioCommand command)
        {
            try
            {
                var usuarioDto = await _mediator.Send(command);
                return Ok(usuarioDto);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<AuthResponseDto>> Login([FromBody] LoginUsuarioCommand command)
        {
            try
            {
                var authResponse = await _mediator.Send(command);
                return Ok(authResponse);
            }
            catch (Exception e)
            {
                return Unauthorized(e.Message);
            }
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<AuthResponseDto>> RefreshToken([FromBody] RefreshTokenCommand command)
        {
            try
            {
                var authResponse = await _mediator.Send(command);
                return Ok(authResponse);
            }
            catch (Exception e)
            {
                return Unauthorized(e.Message);
            }
        }
    }
}
