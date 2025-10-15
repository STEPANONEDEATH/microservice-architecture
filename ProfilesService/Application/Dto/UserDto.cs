namespace ProfilesService.Application.Dto;

public record UserDto(Guid Id, string Username, string Email, string Role, decimal Balance);