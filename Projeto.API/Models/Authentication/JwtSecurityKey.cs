using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Projeto.API.Models.Authentication
{
    public class JwtSecurityKey
    {
        public static SymmetricSecurityKey Create(string secret)
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret));
        }
    }
}
