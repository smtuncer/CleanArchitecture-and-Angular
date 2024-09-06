using CleanArchitecture.Application.Features.AuthFeatures.Commands.Login;
using MediatR;
using TS.Result;

namespace CleanArchitecture.Application.Features.AuthFeatures.Commands.CreateNewTokenByRefreshToken;

public sealed record CreateNewTokenByRefreshTokenCommand(
    string UserId,
    string RefreshToken) : IRequest<Result<LoginCommandResponse>>;
