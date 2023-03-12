﻿using System.IdentityModel.Tokens.Jwt;

namespace Projeto.API.Models.Authentication
{
    public class TokenJWT
    {
        private readonly JwtSecurityToken token;

        internal TokenJWT(JwtSecurityToken token)
        {
            this.token = token;
        }

        public DateTime ValidTo => token.ValidTo;

        public string Value => new JwtSecurityTokenHandler().WriteToken(token);
    }
}
