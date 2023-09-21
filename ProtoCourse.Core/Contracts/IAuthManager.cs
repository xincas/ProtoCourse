using Microsoft.AspNetCore.Identity;
using ProtoCourse.Core.Models.User;

namespace ProtoCourse.Core.Contracts;

public interface IAuthManager
{
    Task<IEnumerable<IdentityError>> Register(UserDto userDto);
    Task<AuthResponseDto> Login(LoginDto loginDto);
    Task<string> CreateRefreshToken();
    Task<AuthResponseDto> VerifyRefreshToken(AuthResponseDto request);
}
