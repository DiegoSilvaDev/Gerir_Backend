using Senai.Gerir.Api.Dominios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Gerir.Api.Interfaces
{
    interface ITarefaRepositorio
    {

        /// <summary>
        /// Cadastra uma tarefa
        /// </summary>
        /// <param name="tarefa">Parâmetro que contem as informações que seram cadastradas na tarefa</param>
        /// <returns>Retorna a tarefa cadastrada</returns>
        Tarefa Cadastrar(Tarefa tarefa);

        /// <summary>
        /// Utilizado para listar as tarefas existentes independente de seu status
        /// </summary>
        /// <param name="idUsuario">id dos usuários</param>
        /// <returns>Retorna todas as tarefas existentes em lista</returns>
        List<Tarefa> ListarTodas(Guid idUsuario);

        /// <summary>
        /// Altera o status da tarefa 
        /// </summary>
        /// <param name="idTarefa">ID da tarefa a ser alterada</param>
        /// <returns>Retorna a tarefa com o status modificado</returns>
        Tarefa AlterarStatus(Guid idTarefa);

        /// <summary>
        /// Remove a tarefa a partir de seu ID
        /// </summary>
        /// <param name="idTarefa">ID da tarefa</param>
        void Remover(Guid idTarefa);

        /// <summary>
        /// Edita a tarefa da forma que for preferida
        /// </summary>
        /// <param name="tarefa">Contem os dados á serem alterados</param>
        /// <returns>Edita a tarefa </returns>
        Tarefa Editar(Tarefa tarefa);

        /// <summary>
        /// Efetua uma busca a partir do id da tarefa
        /// </summary>
        /// <param name="idTarefa">ID da tarefa</param>
        /// <returns>Retorna a tarefa que foi buscada</returns>
        Tarefa BuscarPorId(Guid idTarefa);




    }
}
