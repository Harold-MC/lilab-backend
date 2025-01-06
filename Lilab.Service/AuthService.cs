using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FluentEmail.Core;
using Lilab.Data.Contract;
using Lilab.Data.Entity;
using Lilab.Data.Setting;
using Lilab.Data.ViewModel;
using Lilab.Service.Contract;
using Lilab.Service.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Lilab.Service
{
    public class AuthService: IAuthService
    {
        private readonly IRepository<User> _userRepository;
        private readonly PasswordHasher _passwordHasher;
        private readonly JwtSetting _jwtSetting;
        private readonly IFluentEmail _fluentEmail;

        public AuthService(
            IRepository<User> userRepository,
            JwtSetting jwtSetting,
            // IFluentEmail fluentEmail,
            PasswordHasher passwordHasher)
        {
            _jwtSetting = jwtSetting;
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            // _fluentEmail = fluentEmail;
        }

        private string GenerateToken(User user)
        {
            var issuer = _jwtSetting.Issuer;
            var audience = _jwtSetting.Audience;
            var key = Encoding.ASCII.GetBytes
                (Environment.GetEnvironmentVariable("JWT_SECRET_KEY")!);
            
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Name),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti,
                        Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials
                (new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha512Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var stringToken = tokenHandler.WriteToken(token);

            return stringToken;
        }

        public async Task<LoginResponseViewModel> AuthenticateAsync(LoginViewModel model)
        {
            var user = await _userRepository.GetAsync(query => query
                .Include(user => user.Role)
                .ThenInclude(role => role.Permissions)
                .Where(user => user.Email == model.Username));

            var errorMessage = "Usuario y/o contraseña incorrecta";

            if (user is null || !user.IsActive) throw new Exception(errorMessage);

            var (verified, _) = _passwordHasher.Check(user.Password, model.Password);
            
            if(!verified) throw new Exception(errorMessage);

            var token = GenerateToken(user);

            return new LoginResponseViewModel(user, token);

        }
    }
}