using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentralEstagios.Repositorio
{
    interface IRepositorio<TEntity> where TEntity : class
    {
        /// <summary>
        /// Criar Registro
        /// </summary>
        /// <param name="entity"></param>
        void Criar(TEntity entity);
        /// <summary>
        /// Obter Registro
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> Obter();
        /// <summary>
        /// Atualizar Registro
        /// </summary>
        /// <param name="entity"></param>
        void Atualizar(TEntity entity);
        /// <summary>
        /// Excluir Registro
        /// </summary>
        /// <param name="entity"></param>
        void Excluir(TEntity entity);
    }
}
