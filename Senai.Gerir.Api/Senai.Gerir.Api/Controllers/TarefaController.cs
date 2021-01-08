using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.Gerir.Api.Dominios;
using Senai.Gerir.Api.Interfaces;
using Senai.Gerir.Api.Repositorios;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Gerir.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarefaController : ControllerBase
    {
        private readonly ITarefaRepositorio _tarefaRepositorio;

        public TarefaController()
        {
            _tarefaRepositorio = new TarefaRepositorio();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Cadastrar(Tarefa tarefa)
        {
            try
            {
                //Pega o valor do usuário que esta logado
                var usuarioId = HttpContext.User.Claims.FirstOrDefault(
                                c => c.Type == JwtRegisteredClaimNames.Jti
                            );

                //Atribui o usuario a tarefa
                tarefa.UsuarioId = new Guid(usuarioId.Value);
                
                //Cadastra a tarefa
                _tarefaRepositorio.Cadastrar(tarefa);


                return Ok(tarefa);
            }
            catch (Exception ex) 
            {

                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet]
        public IActionResult Listar()
        {
            try
            {

                var usuarioId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti);

                var tarefas = _tarefaRepositorio.ListarTodas(new Guid(usuarioId.Value));

                return Ok(new { data = tarefas });
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPut("{idTarefa}")]
        public IActionResult Editar(Guid idTarefa, Tarefa tarefa)
        {
            try
            {
                //Pega o valor do usuário que esta logado
                var usuarioId = HttpContext.User.Claims.FirstOrDefault(
                                c => c.Type == JwtRegisteredClaimNames.Jti
                            );


                //Efetua uma busca por tarefa a partir do ID da própria
                var tarefaExiste = _tarefaRepositorio.BuscarPorId(idTarefa  );

                //Verifica se realmente existe uma tarefa com o ID que foi enviado
                if (tarefaExiste == null)
                    return NotFound();

                //Verifica se a tarefa é a do usuario logado
                if (tarefaExiste.UsuarioId != new Guid(usuarioId.Value))
                    return Unauthorized("Usuario não tem permissão");

                //Envia para o metodo editar os dados do usuário recebido
                tarefa.Id = idTarefa;
                _tarefaRepositorio.Editar(tarefa);

                return Ok(tarefa);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("Buscar/{idTarefa}")]
        public IActionResult Buscar(Guid idTarefa)
        {
            try
            {

                var usuarioId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti);

                //Pega as informações da tarefa a partir do seu ID
                var tarefa = _tarefaRepositorio.BuscarPorId(idTarefa);

                //Verifica se a tarefa existe, caso contrário retorna NotFound()
                if (tarefa == null)
                    return NotFound();


                //Verifica se a tarefa é do usuario que está logado(utilizando o método)
                if (tarefa.UsuarioId != new Guid(usuarioId.Value))
                    return Unauthorized("Usuario não tem permissão");


                return Ok(tarefa);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [Authorize]
        [HttpDelete ("{idTarefa}")]
        public IActionResult Excluir(Guid idTarefa)
        {
            try
            {
                //Pega o valor do usuário que esta logado
                var usuarioid = HttpContext.User.Claims.FirstOrDefault(
                                c => c.Type == JwtRegisteredClaimNames.Jti
                            );

                //Busca uma tarefa pelo seu Id
                var tarefaexiste = _tarefaRepositorio.BuscarPorId(idTarefa);

                //Verifica se a tarefa existe
                if (tarefaexiste == null)
                    return NotFound();

                //Verifica se a tarefa é do usuário logado
                if (tarefaexiste.UsuarioId != new Guid(usuarioid.Value))
                    return Unauthorized("Usuário não tem permissão");

                _tarefaRepositorio.Remover(idTarefa);

                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPut("AlterarStatus/{idTarefa}")]
        public IActionResult AlterarStatus(Guid idTarefa)
        {
            try
            {

                var tarefa = _tarefaRepositorio.BuscarPorId(idTarefa);

                var usuarioId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti);


                if (tarefa == null)
                    return NotFound();


                if (tarefa.UsuarioId != new Guid(usuarioId.Value))
                    return Unauthorized("Usuario não tem permissão");

                _tarefaRepositorio.AlterarStatus(idTarefa);
                
                return Ok(tarefa);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }


    }
}
