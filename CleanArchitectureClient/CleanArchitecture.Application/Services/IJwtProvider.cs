using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Services;

public interface IJwtProvider
{
    Task<string> CreateTokenAsync(User user);
}