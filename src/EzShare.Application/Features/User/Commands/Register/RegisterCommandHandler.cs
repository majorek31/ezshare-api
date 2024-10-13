using System.Net;
using EzShare.Application.Common;
using EzShare.Application.Contracts.Repositories;
using Mapster;
using MediatR;

namespace EzShare.Application.Features.User.Commands.Register;

public class RegisterCommandHandler(IRoleRepository roleRepository, IUserRepository userRepository) : IRequestHandler<RegisterCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var isEmailUnique = await userRepository.IsEmailUniqueAsync(request.Dto.Email);
        if (!isEmailUnique) return Result<Unit>.Failure("This user already exists", HttpStatusCode.Conflict);
        
        var user = request.Dto.Adapt<Domain.Entities.User>();
        
        var role = await roleRepository.GetByNameAsync("User");
        if (role is null) return Result<Unit>.Failure("Role not found", HttpStatusCode.NotFound);
        
        user.RoleId = role.Id;
        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Dto.Password);
        
        await userRepository.AddAsync(user);
        
        return Result<Unit>.Success(Unit.Value, HttpStatusCode.Created);
    }
}