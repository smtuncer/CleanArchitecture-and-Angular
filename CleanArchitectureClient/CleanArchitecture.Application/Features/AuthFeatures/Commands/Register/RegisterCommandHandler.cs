using AutoMapper;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using TS.Result;

namespace CleanArchitecture.Application.Features.AuthFeatures.Commands.Register;

public sealed class RegisterCommandHandler(
    UserManager<User> userManager,
    IMapper mapper,
    IMailService mailService) : IRequestHandler<RegisterCommand, Result<string>>
{

    public async Task<Result<string>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        User user = mapper.Map<User>(request);
        IdentityResult result = await userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
        {
            throw new Exception(result.Errors.First().Description);
        }

        List<string> emails = new();
        emails.Add(request.Email);
        string body = "";

        await mailService.SendMailAsync(emails, "Mail Onayı", body);

        return Result<string>.Succeed("Kullanıcı kaydı başarıyla tamamlandı");
    }
}
