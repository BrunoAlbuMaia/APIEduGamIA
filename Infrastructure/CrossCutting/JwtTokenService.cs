using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain.Entitys;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.CrossCutting
{
    public class JwtTokenService
    {
        private readonly string _secretKey;

        public JwtTokenService(string secretKey)
        {
            _secretKey = secretKey;
        }
        public string GenerateJwtToken(UsersEntity usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, usuario.UserCompl.username),
                    new Claim("userId", usuario.id.ToString()),
                    new Claim("role",usuario.UserCompl.role)
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public string GetUserIdFromToken(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();

                // Valida se o token é um JWT válido
                if (tokenHandler.CanReadToken(token))
                {
                    // Decodifica o token sem validar a assinatura (útil para extrair informações)
                    var jwtToken = tokenHandler.ReadJwtToken(token);

                    // Busca a claim "UsuarioId"
                    var userIdClaim = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "UserId");

                    if (userIdClaim != null)
                    {
                        return userIdClaim.Value;
                    }
                }

                return null; // Retorna null se o token for inválido ou não contiver o UserId
            }
            catch (Exception ex)
            {
                // Trate exceções, como token malformado
                Console.WriteLine($"Erro ao decodificar o token: {ex.Message}");
                return null;
            }
        }

    }


}
