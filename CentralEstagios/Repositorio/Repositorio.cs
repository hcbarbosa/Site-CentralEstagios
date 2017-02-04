using CentralEstagios.Contexto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CentralEstagios.Repositorio
{
    public class Repositorio<TEntity> : IDisposable, IRepositorio<TEntity> where TEntity : class
    {
        /// <summary>
        /// Contexto corrente
        /// </summary>
        protected ProjetoContexto contexto = new ProjetoContexto();

        /// <summary>
        /// Criar Registro
        /// </summary>
        /// <param name="entity"></param>
        public void Criar(TEntity entity)
        {
            contexto.Set<TEntity>().Add(entity);
            contexto.SaveChanges();
        }

        /// <summary>
        /// Obter Registro
        /// </summary>
        /// <returns></returns>
        public IQueryable<TEntity> Obter()
        {
            return contexto.Set<TEntity>();
        }

        /// <summary>
        /// Atualizar Registro
        /// </summary>
        /// <param name="entity"></param>
        public void Atualizar(TEntity entity)
        {
            contexto.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            contexto.SaveChanges();
        }

        /// <summary>
        /// Excluir Registro
        /// </summary>
        /// <param name="entity"></param>
        public void Excluir(TEntity entity)
        {
            contexto.Set<TEntity>().Remove(entity);
            contexto.SaveChanges();
        }

        /// <summary>
        /// Destroi da Classe
        /// </summary>
        public void Dispose()
        {
            this.contexto.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}