using CleanArchitecture.Application.Features.AuthFeatures.Commands.Login;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using TS.Result;

namespace CleanArchitecture.Application.Features.AuthFeatures.Commands.CreateNewTokenByRefreshToken;

public sealed class CreateNewTokenByRefreshTokenCommandHandler(
    UserManager<User> userManager,
    IJwtProvider jwtProvider) : IRequestHandler<CreateNewTokenByRefreshTokenCommand, Result<LoginCommandResponse>>
{

    public async Task<Result<LoginCommandResponse>> Handle(CreateNewTokenByRefreshTokenCommand request, CancellationToken cancellationToken)
    {
        User user = await userManager.FindByIdAsync(request.UserId);
        if (user == null) throw new Exception("Kullanıcı bulunamadı!");

        if (user.RefreshToken != request.RefreshToken)
            throw new Exception("Refresh Token geçerli değil!");

        if (user.RefreshTokenExpires < DateTime.Now)
            throw new Exception("Refresh Tokenun süresi dolmuş!");

        string token = await jwtProvider.CreateTokenAsync(user);
        LoginCommandResponse response = new(token);

        return Result<LoginCommandResponse>.Succeed(response);
    }
}
