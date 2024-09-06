﻿using CleanArchitecture.Application.Dtos;
using MediatR;

namespace CleanArchitecture.Application.Features.AuthFeatures.Commands.Register;

public sealed record RegisterCommand(
    string Email,
    string UserName,
    string FirstName,
    string LastName,
    string Password): IRequest<MessageResponse>;
