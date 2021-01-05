using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Senai.Gerir.Api.Dominios;
using Senai.Gerir.Api.Interfaces;
using Senai.Gerir.Api.Repositorios;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Senai.Gerir.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public UsuarioController()
        {
            _usuarioRepositorio = new UsuarioRepositorio();
        }

        [HttpPost]
        public IActionResult Cadastrar(Usuario usuario)
        {
            try
            {
                _usuarioRepositorio.Cadastrar(usuario);

                return Ok(usuario);
            }
            catch (System.Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }
    
    
        [HttpPost("login")]
        public IActionResult Logar(Usuario usuario)
        {
            try
            {
                //Verifica se o usuário existe
                var usuarioexiste = _usuarioRepositorio
                                        .Logar(usuario.Email,usuario.Senha);

                //Caso não exista retorna NotFound
                if(usuarioexiste == null)
                   return NotFound();


                //Caso exista retorna o token gerado a partir dos dados referentes ao usuário
                var token = GerarJsonWebToken(usuarioexiste);

                //Retorna sucesso com o Token do Usuário
                return Ok(token);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// Busca as informações do usuário pelo id
        /// </summary>
        /// <returns>Retorna o usuário/informações do próprio</returns>
        [Authorize]
        [HttpGet]
        public IActionResult MeusDados()
        {
            try
            {
                //Pega as informações referentes ao usuário nas claims
                var claimsUsuario = HttpContext.User.Claims;

                //Pega o id do usuário na Claim Jti
                var usuarioId = claimsUsuario.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti);

                //Pega as informações do usuário a partir do seu ID
                var usuario = _usuarioRepositorio.BuscarPorId(new Guid(usuarioId.Value));

                return Ok(usuario);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPut]
        public IActionResult Editar(Usuario usuario)
        {
            try
            {
                //Pega as informações referentes ao usuário nas claims
                var claimsUsuario = HttpContext.User.Claims;

                //Pega o id do usuário na Claim Jti
                var usuarioId = claimsUsuario.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti);

                //Atribuo o valor do usuarioId ao id do usuário recebido
                usuario.Id = new Guid(usuarioId.Value);

                //Envia para o metodo editar os dados do usuário recebido
                _usuarioRepositorio.Editar(usuario);
                
                return Ok(usuario);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpDelete]
        public IActionResult Excluir()
        {
            try
            {
                //Pega as informações referentes ao usuário nas claims
                var claimsUsuario = HttpContext.User.Claims;

                //Pega o id do usuário na Claim Jti
                var usuarioId = claimsUsuario.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti);

                _usuarioRepositorio.Remover(new Guid(usuarioId.Value));

                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        private string GerarJsonWebToken(Usuario usuario)
        {
            //Chave de Segurança 
            var chaveSegurança = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("GerirChaveSeguranca"));

            //Define as credênciais
            var credenciais = new SigningCredentials(chaveSegurança, SecurityAlgorithms.HmacSha256);


            var data = new[]
            {
                new Claim(JwtRegisteredClaimNames.FamilyName, usuario.Nome),
                new Claim(JwtRegisteredClaimNames.Email, usuario.Email),
                new Claim(ClaimTypes.Role, usuario.Tipo),
                new Claim(JwtRegisteredClaimNames.Jti, usuario.Id.ToString())
            };

            var token = new JwtSecurityToken(
                    "gerir.com.br",
                    "gerir.com.br",
                    data,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: credenciais
            );



            return new JwtSecurityTokenHandler().WriteToken(token) ;
        }
    }
}
