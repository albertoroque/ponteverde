using ponteverdeCore.Models.BusinessModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ponteverdeCore.Models.Repositories.Interfaces
{
    public interface IPaisRepository
    {
        pais Obter(long id);

        IQueryable<pais> Obter();

        IQueryable<pais> Obter(Expression<Func<pais, bool>> expressao);

        void Criar(pais pais);
    }
}
