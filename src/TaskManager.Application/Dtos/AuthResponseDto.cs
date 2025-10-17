﻿namespace TaskManager.Application.Dtos;

public class AuthResponseDto
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
}
