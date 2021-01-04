using Senai.Gerir.Api.Dominios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Gerir.Api.Interfaces
{
    interface IUsuarioRepositorio
    {
        /// <summary>
        /// Cadastra um novo Usuário
        /// </summary>
        /// <param name="usuario">Contém os dados do novo usuário</param>
        /// <returns>Retorna o usuário cadastrado</returns>
        Usuario Cadastrar(Usuario usuario);

        /// <summary>
        /// Loga o usuário no sistema
        /// </summary>
        /// <param name="email">Email do usuário</param>
        /// <param name="senha">Senha do usuário</param>
        /// <returns>Retorna o usuário caso o encontre no BD</returns>
        Usuario Logar(string email, string senha);

        /// <summary>
        /// Edita o usuário
        /// </summary>
        /// <param name="usuario">Contém os dados do usuário</param>
        /// <returns>Retorna a alteração feita nos dados do usuário</returns>
        Usuario Editar(Usuario usuario);

        /// <summary>
        /// Remove um usuário
        /// </summary>
        /// <param name="id">Id do usuário</param>
        void Remover(Guid id);

        /// <summary>
        /// Efetua uma busca a partir do id do Usuário
        /// </summary>
        /// <param name="id">Id do usuário</param>
        /// <returns>Retprna as informações do usuário através do seu id</returns>
        Usuario BuscarPorId(Guid id);
    }
}
