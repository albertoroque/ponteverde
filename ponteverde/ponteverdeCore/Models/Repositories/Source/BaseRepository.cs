using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;
using ponteverdeCore.Models.BusinessModels;
using ponteverdeCore.Models.Repositories.Interfaces;

namespace ponteverdeCore.Models.Repositories.Source
{
    public class BaseRepository<TPrimaryEntity, TDependentEntity> : IBaseRepository<TPrimaryEntity, TDependentEntity>
         where TPrimaryEntity : class, IEntity
         where TDependentEntity : class
    {
        protected readonly PvCoreEntities bd;
        protected readonly DbSet<TPrimaryEntity> PrimaryDatabaseSet;
        protected readonly DbSet<TDependentEntity> DependentDatabaseSet;

        public BaseRepository(PvCoreEntities _bd)
        {
            this.bd = _bd;
            this.PrimaryDatabaseSet = bd.Set<TPrimaryEntity>();
            this.DependentDatabaseSet = bd.Set<TDependentEntity>();
        }

        public virtual TPrimaryEntity Obter(long id)
        {
            try
            {
                this.bd.Configuration.AutoDetectChangesEnabled = false;
                return PrimaryDatabaseSet.Find(id);
            }
            finally
            {
                this.bd.Configuration.AutoDetectChangesEnabled = true;
            }
        }

        public virtual IQueryable<TPrimaryEntity> Obter()
        {
            return this.PrimaryDatabaseSet;
        }

        public virtual IQueryable<TPrimaryEntity> Obter(Expression<Func<TPrimaryEntity, bool>> expressao)
        {
            return this.Obter().Where(expressao);
        }

        public virtual void Criar(TPrimaryEntity Objeto)
        {
            if (Objeto == null)
            {
                throw new ArgumentNullException("Objeto", "Não é possível adicionar entidade com valores nulos.");
            }

            this.PrimaryDatabaseSet.Add(Objeto);
        }

        public virtual void Criar(TPrimaryEntity ObjetoPrimario, TDependentEntity ObjetoDependente)
        {
            if (ObjetoPrimario == null)
            {
                throw new ArgumentNullException("ObjetoPrimario", "Não é possível adicionar entidade com valores nulos.");
            }
            if (ObjetoDependente == null)
            {
                throw new ArgumentNullException("ObjetoDependente", "Não é possível adicionar entidade com valores nulos.");
            }
            this.Criar(ObjetoPrimario);
            this.DependentDatabaseSet.Attach(ObjetoDependente); // atacha
            bd.Entry(ObjetoDependente).State = System.Data.EntityState.Added; //Insere
        }

        public virtual void Excluir(TPrimaryEntity Objeto)
        {
            if (Objeto == null)
            {
                throw new ArgumentNullException("Objeto", "Não é possível excluir entidade com valores nulos.");
            }
            this.PrimaryDatabaseSet.Remove(Objeto);
        }

        public virtual void Excluir(Expression<Func<TPrimaryEntity, bool>> expressao)
        {
            var records = this.PrimaryDatabaseSet.Where(expressao);
            foreach (var rec in records)
            {
                this.Excluir(rec);
            }
        }

        public virtual void Editar(TPrimaryEntity Objeto)
        {
            this.Update<TPrimaryEntity>(Objeto, Objeto.id);
        }

        public virtual void Editar(TPrimaryEntity ObjetoPrimario, TDependentEntity ObjetoDependente)
        {
            this.Editar(ObjetoPrimario);
            this.Update<TDependentEntity>(ObjetoDependente, ObjetoPrimario.id);
        }

        private void Update<T>(T model, long key) where T : class
        {
            if (model == null)
            {
                throw new ArgumentNullException(model.ToString(), "Não é possível atualizar objeto com valores nulos.");
            }
            var entry = this.bd.Entry(model);
            if (entry.State == System.Data.EntityState.Detached)
            {
                var currentEntry = this.bd.Set<T>().Find(key);
                if (currentEntry != null)
                {
                    var attachedEntry = this.bd.Entry(currentEntry);
                    attachedEntry.CurrentValues.SetValues(model);
                }
                else
                {
                    this.bd.Set<T>().Attach(model);
                    entry.State = System.Data.EntityState.Modified;
                }
            }

        }
    }
}
