using ponteverdeCore.Models.BusinessModels;
using ponteverdeCore.Models.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ponteverdeCore.Models.Repositories.Source
{
    internal class PaisRepository : BaseRepository<pais, object>, IPaisRepository
    {
        public PaisRepository(PvCoreEntities bd) 
            : base(bd)
        {

        }
    }
}
