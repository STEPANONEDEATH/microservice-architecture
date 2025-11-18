using MediatR;
using ProfilesService.Application.Dto;
using ProfilesService.Application.Interfaces;
using ProfilesService.Application.UseCases;

namespace ProfilesService.Api.Handlers;

public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, UserDto>
{
    private readonly IUserService _service;

    public RegisterUserHandler(IUserService service)
    {
        _service = service;
    }

    public async Task<UserDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        return await _service.RegisterAsync(request.Username, request.Email, request.Password);
    }
}