using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Projeto.API.Models.Authentication;
using Projeto.Entities.Entities.Authentication;
using System.Text;

namespace Projeto.API.Controllers.Authentication
{
    [Route("api/v1/authentication")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UsersController(
            UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager
        )
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [AllowAnonymous]
        [Produces("application/json")]
        [HttpPost("login")]
        public async Task<IActionResult> CriarTokenIdentity([FromBody] LoginViewModel login) 
        {
            if (string.IsNullOrWhiteSpace(login.Email) || string.IsNullOrWhiteSpace(login.Senha))
            {
                return BadRequest("Faltam alguns dados");
            }

            var resultado = await _signInManager.PasswordSignInAsync(
                login.Email, login.Senha, false, lockoutOnFailure: false);

            if (resultado.Succeeded)
            {
                var currentUser = await _userManager.FindByEmailAsync(login.Email);
                var userId = currentUser.Id;

                var token = new TokenJWTBuilder()
                    .AddSecurityKey(JwtSecurityKey.Create("634746795957356E59584A705933563061584A7062576C79636E5668636D383D"))
                    .AddSubject("Projeto - Exemplo de estrutura DDD/TDD")
                    .AddIssuer("Projeto.Security.Bearer")
                    .AddAudience("Projeti.Security.Bearer")
                    .AddClaim("userId", userId)
                    .AddExpiry(525600)
                    .Builder();

                var response = new
                {
                    user = new
                    {
                        username = currentUser.UserName,
                        email = currentUser.Email,
                        tipo = currentUser.Tipo
                    },
                    token = token.Value
                };

                return Ok(response);
            }

            return Unauthorized();
        }

        [AllowAnonymous]
        [Produces("application/json")]
        [HttpPost("register")]
        public async Task<IActionResult> AdicionarUsuarioIdentity([FromBody] RegisterViewModel login)
        {
            if (
                string.IsNullOrWhiteSpace(login.Email) || 
                string.IsNullOrWhiteSpace(login.Senha) ||
                string.IsNullOrWhiteSpace(login.Cpf)
            )
            {
                return BadRequest("Faltam alguns dados!");
            }

            var user = new ApplicationUser
            {
                UserName = login.Email,
                Email = login.Email,
                Cpf = login.Cpf,
                Tipo = login.Tipo,
            };

            var resultado = await _userManager.CreateAsync(user, login.Senha);

            if (resultado.Errors.Any())
            {
                return BadRequest(resultado.Errors);
            }

            var userId = await _userManager.GetUserIdAsync(user);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var resultado2 = await _userManager.ConfirmEmailAsync(user, code);

            if (resultado2.Succeeded)
                return Ok("Usuário adicionado com sucesso!");
            else
                return BadRequest("Erro ao adicionar usuário");
        }
    }
}
