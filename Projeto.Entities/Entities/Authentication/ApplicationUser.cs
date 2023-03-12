using Microsoft.AspNetCore.Identity;
using Projeto.Entities.Enums.Authentication;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projeto.Entities.Entities.Authentication
{
    public class ApplicationUser : IdentityUser
    {
        [Column(name: "USR_CPF")]
        public string Cpf { get; set; }

        [Column(name: "USR_TIPO")]
        public TipoUsuario? Tipo { get; set; }
    }
}
