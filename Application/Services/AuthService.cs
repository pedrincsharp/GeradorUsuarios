using Application.DTO.User;
using Application.Services;
using Application.Services.Interface;
using Infrastructure.Repository.Interface;
using Application.DTO.User;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;

    public AuthService(IUserRepository userRepository, ITokenService tokenService)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
    }

    public async Task<TokenDTO> Login(AuthUserDTO dto)
    {
        var user = await _userRepository.GetByUsername(dto.Username)
            ?? throw new ArgumentException("Usuário ou senha inválidos");

        if (user.Password != dto.Password)
            throw new ArgumentException("Usuário ou senha inválidos");

        return _tokenService.Generate(user);
    }
}
