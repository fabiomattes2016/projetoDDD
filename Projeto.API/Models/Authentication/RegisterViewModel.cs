using System.Text.Json.Serialization;
using Projeto.Entities.Enums.Authentication;

namespace Projeto.API.Models.Authentication
{
    public class RegisterViewModel
    {
        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("senha")]
        public string Senha { get; set; }

        [JsonPropertyName("cpf")]
        public string Cpf { get; set; }

        [JsonPropertyName("tipo_usuario")]
        public TipoUsuario? Tipo { get; set; }
    }
}
