using MediatR;
using ProfilesService.Application.Dto;

namespace ProfilesService.Application.UseCases;

public record RegisterUserCommand(string Username, string Email, string Password)
    : IRequest<UserDto>;