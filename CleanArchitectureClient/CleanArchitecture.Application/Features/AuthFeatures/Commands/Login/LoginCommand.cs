using MediatR;
using TS.Result;

namespace CleanArchitecture.Application.Features.AuthFeatures.Commands.Login;

public sealed record LoginCommand(
    string UserNameOrEmail,
    string Password) : IRequest<Result<LoginCommandResponse>>;
