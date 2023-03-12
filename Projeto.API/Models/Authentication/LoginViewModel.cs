using System.Text.Json.Serialization;
using Projeto.Entities.Enums.Authentication;

namespace Projeto.API.Models.Authentication
{
    public class LoginViewModel
    {
        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("senha")]
        public string Senha { get; set; }
    }
}
