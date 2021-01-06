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

        [HttpPost]
        public IActionResult Cadastrar(Tarefa tarefa)
        {
            try
            {
                _tarefaRepositorio.Cadastrar(tarefa);


                return Ok(tarefa);
            }
            catch (Exception ex) 
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult Listar(Guid idUsuario)
        {
            try
            {

                var tarefas = _tarefaRepositorio.ListarTodas(idUsuario);

                return Ok(tarefas);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [Authorize]
        [HttpPut]
        public IActionResult Editar(Tarefa tarefa)
        {
            try
            { 

                //Envia para o metodo editar os dados do usuário recebido
                _tarefaRepositorio.Editar(tarefa);

                return Ok(tarefa);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("Buscar/{id}")]
        public IActionResult Buscar(Guid id)
        {
            try
            { 
                //Pega as informações da tarefa a partir do seu ID
                var tarefa = _tarefaRepositorio.BuscarPorId(id);

                return Ok(tarefa);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [Authorize]
        [HttpDelete]
        public IActionResult Excluir(Guid id)
        {
            try
            {
                /* //Pega as informações referentes ao usuário nas claims
                var claimsUsuario = HttpContext.User.Claims;

                //Pega o id do usuário na Claim Jti
                var tarefaId = claimsUsuario.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti);

                //Pega as informações do usuário a partir do seu ID
                _tarefaRepositorio.BuscarPorId(new Guid(tarefaId.Value));*/

                _tarefaRepositorio.Remover(id);

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

                var tarefa = _tarefaRepositorio.AlterarStatus(idTarefa);
                
                return Ok(tarefa);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }


    }
}
