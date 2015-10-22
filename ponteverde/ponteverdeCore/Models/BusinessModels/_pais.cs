using ponteverdeCore.Models.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ponteverdeCore.Models.BusinessModels
{
    [MetadataType(typeof(paisMetadata))]
    public partial class pais : IEntity
    {
    }

    public class paisMetadata
    {

    }
}
