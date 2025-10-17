using MediatR;
using TaskManager.Application.Common.Exceptions;
using TaskManager.Application.Dtos;
using TaskManager.Domain.Interfaces.UnitOfWork;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using TaskManager.Application.Features.Usuario.Command;

namespace TaskManager.Application.Features.Usuario.Handlers;

public class LoginUsuarioCommandHandler : IRequestHandler<LoginUsuarioCommand, AuthResponseDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IConfiguration _configuration;

    public LoginUsuarioCommandHandler(IUnitOfWork unitOfWork, IConfiguration configuration)
    {
        _unitOfWork = unitOfWork;
        _configuration = configuration;
    }

    public async Task<AuthResponseDto> Handle(LoginUsuarioCommand request, CancellationToken cancellationToken)
    {
        var usuario = await _unitOfWork.UsuarioRepository.GetByLoginSenhaAsync(request.Login, request.Senha);

        if (usuario == null)
        {
            throw new NotFoundException("Usuário ou senha inválidos.");
        }

        var token = GenerateJwtToken(usuario.Id, usuario.Nome);
        var refreshToken = GenerateRefreshToken();

        usuario.RefreshToken = refreshToken;
        usuario.RefreshTokenExpiryTime = DateTime.Now.AddDays(7); // Exemplo: refresh token expira em 7 dias
        await _unitOfWork.UsuarioRepository.UpdateAsync(usuario);
        await _unitOfWork.CommitAsync();

        return new AuthResponseDto
        {
            Id = usuario.Id,
            Nome = usuario.Nome,
            Token = token,
            RefreshToken = refreshToken
        };
    }

    private string GenerateJwtToken(int userId, string userName)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, userName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim("userId", userId.ToString())
        };

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["Jwt:AccessTokenExpirationMinutes"])),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private string GenerateRefreshToken()
    {
        return Guid.NewGuid().ToString();
    }
}
