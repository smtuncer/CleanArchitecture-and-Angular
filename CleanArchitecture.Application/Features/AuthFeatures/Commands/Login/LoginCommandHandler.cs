using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace CleanArchitecture.Application.Features.AuthFeatures.Commands.Login;
internal sealed class LoginCommandHandler(
    UserManager<User> userManager,
    IJwtProvider jwtProvider) : IRequestHandler<LoginCommand, Result<LoginCommandResponse>>
{
    public async Task<Result<LoginCommandResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        User? appUser =
            await userManager.Users.FirstOrDefaultAsync(p =>
            p.UserName == request.UserNameOrEmail ||
            p.Email == request.UserNameOrEmail, cancellationToken);

        if (appUser is null)
        {
            return Result<LoginCommandResponse>.Failure("Kullanıcı Bulunamadı");
        }

        bool isPasswordCorrect = await userManager.CheckPasswordAsync(appUser, request.Password);
        if (!isPasswordCorrect)
        {
            return Result<LoginCommandResponse>.Failure("Parola Yanlış");
        }

        string token = await jwtProvider.CreateTokenAsync(appUser);
        LoginCommandResponse response = new(token);

        return Result<LoginCommandResponse>.Succeed(response);
    }
}