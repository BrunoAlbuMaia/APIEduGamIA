using Domain.Entitys;
using Domain.Interfaces;
using Infrastructure.CrossCutting;
using Infrastructure.CrossCutting.Redis;
using Infrastructure.Data.Repositories.Interfaces;
using Newtonsoft.Json;

namespace Service
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRedisService _redis;
        private readonly JwtTokenService _jwtToken;

        public AuthenticationService(IUserRepository userRepository, IRedisService redis)
        {
            _userRepository = userRepository;
            _redis = redis;
            _jwtToken = new JwtTokenService("U3XVAzYsR6CPqkKhIErRzMNFq48rpP7I");
        }
        
        public async Task<dynamic> LoginAsync(string username, string password)
        {
            try
            {

                UsersEntity usuarios = await _userRepository.UserByUsername(username);
                if (usuarios == null || !BCrypt.Net.BCrypt.Verify(password, usuarios.UserCompl.password_hash))
                {
                    throw new UnauthorizedAccessException("Credenciais inválidas.");
                }

                var token = _jwtToken.GenerateJwtToken(usuarios);
                var refreshToken = _jwtToken.GenerateJwtToken(usuarios);

                var userKey = $"user:{token}";
                var userData = new
                {
                    Token = token,
                    RefreshToken = refreshToken,
                    UsuarioId = usuarios.id,
                    Username = usuarios.UserCompl.username,
                    dtExpiration = TimeSpan.FromHours(1),
                    Role = usuarios.UserCompl.role // Exemplo de permissões
                };

                await _redis.SetValueAsync(userKey, JsonConvert.SerializeObject(userData), TimeSpan.FromHours(1)); // Expira em 1 hora

                var refreshTokenKey = $"refreshToken:{usuarios.id}";
                await _redis.SetValueAsync(refreshTokenKey, System.Text.Json.JsonSerializer.Serialize(userData), TimeSpan.FromDays(7));

                var authResponse = new
                {
                    Token = token,
                    RefreshToken = refreshToken,
                    Username = usuarios.UserCompl.username,
                    Role = usuarios.UserCompl.role

                };

                return authResponse;



            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<(bool IsAuthenticated, string Role, string UserId, string Username)> ValidateAndAuthorizeAsync(string token)
        {
            var redisData = await _redis.GetValueAsync($"user:{token}");

            if (string.IsNullOrEmpty(redisData))
            {
                return (false, null, null, null);
            }


            var userData = JsonConvert.DeserializeObject<dynamic>(redisData);
            var Role = userData.Role.ToString();
            var filialID = userData.FilialId.ToString();
            var userId = userData.UsuarioId.ToString();
            var username = userData.Username.ToString();


            return (true, Role, userId, username);
        }
    }
}
