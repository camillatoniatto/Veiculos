﻿using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Veiculo.Dominio
{
    public static class ServiceToken
    {
        public static string GenerateToken(User user, IEnumerable<string> role)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Settings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Usuario.ToString()),
                    //new Claim(ClaimTypes.Role, role.RoleName.ToString())

                }),
                Expires = DateTime.UtcNow.AddHours(5),
                SigningCredentials =
                new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            

            foreach (var item in role)
            {
                tokenDescriptor.Subject.AddClaim(new Claim(ClaimTypes.Role, item));
            }
            
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
