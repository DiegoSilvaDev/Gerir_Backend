using Senai.Gerir.Api.Contextos;
using Senai.Gerir.Api.Dominios;
using Senai.Gerir.Api.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Gerir.Api.Repositorios
{
    public class TarefaRepositorio : ITarefaRepositorio
    {
        private readonly GerirContext _context;

        public TarefaRepositorio()
        {
            _context = new GerirContext();
        }

        public Tarefa AlterarStatus(Guid idTarefa)
        {
            try
            {
                //Busca a tarefa no BD a partir de seu id
                var tarefaExiste = BuscarPorId(idTarefa);

                //Verifica se a tarefa realmente existe

                if (tarefaExiste == null)
                    throw new Exception("Tarefa não encontrada");

                //Altera os valores da tarefa
                tarefaExiste.Status = !tarefaExiste.Status;

                // Edita ela
                _context.Tarefas.Update(tarefaExiste);

                // E salva
                _context.SaveChanges();

                return tarefaExiste;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public Tarefa BuscarPorId(Guid idTarefa)
        {
            try
            {
                //Busca a tarefa a partir do seu id usando o Find
                var tarefa = _context.Tarefas.Find(idTarefa);

                return tarefa;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public Tarefa Cadastrar(Tarefa tarefa)
        {
            try
            {
                //Responsável por adicionar a tarefa no DBset Tarefas do contexto 
                _context.Tarefas.Add(tarefa);
                //Salva as alterações feitas no contexto(context)
                _context.SaveChanges();

                return tarefa;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Tarefa Editar(Tarefa tarefa)
        {
            try
            {

                //Busca a tarefa no BD
                var tarefaExiste = BuscarPorId(tarefa.Id);

                //Verifica se a tarefa realmente existe

                if (tarefaExiste == null)
                    throw new Exception("Tarefa não encontrada");

                //Altera os valores da tarefa
                tarefaExiste.Titulo = tarefa.Titulo;
                tarefaExiste.Descricao = tarefa.Descricao;
                tarefaExiste.Categoria = tarefa.Categoria;
                tarefaExiste.DataEntrega = tarefa.DataEntrega;


                // Edita os valores
                _context.Tarefas.Update(tarefaExiste);
                // Salva o que foi editado/alterado
                _context.SaveChanges();

                return tarefaExiste;

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public List<Tarefa> ListarTodas(Guid idUsuario)
        {
            try
            {

                //
                var tarefas = _context.Tarefas.Where(c => c.UsuarioId == idUsuario).ToList();

                return tarefas;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public void Remover(Guid idTarefa)
        {
            try
            {
                var tarefa = BuscarPorId(idTarefa);

                _context.Tarefas.Remove(tarefa);
                _context.SaveChanges();

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        
        }
    }
}
