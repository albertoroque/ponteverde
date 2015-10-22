using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace ponteverdeCore.Models.Repositories.Interfaces
{
    public interface IBaseRepository<TPrimaryEntity, TDependentEntity>
    {
        TPrimaryEntity Obter(long id);

        IQueryable<TPrimaryEntity> Obter();
        IQueryable<TPrimaryEntity> Obter(Expression<Func<TPrimaryEntity, bool>> expressao);

        void Criar(TPrimaryEntity Objeto);

        void Criar(TPrimaryEntity ObjetoPrimario, TDependentEntity ObjetoDependente);

        void Excluir(TPrimaryEntity Objeto);

        void Excluir(Expression<Func<TPrimaryEntity, bool>> expressao);

        void Editar(TPrimaryEntity Objeto);

        void Editar(TPrimaryEntity ObjetoPrimario, TDependentEntity ObjetoDependente);
    }
}
