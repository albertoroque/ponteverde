//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ponteverdeCore.Models.BusinessModels
{
    using System;
    using System.Collections.Generic;
    
    public partial class produto
    {
        public produto()
        {
            this.produtofavorito = new HashSet<produtofavorito>();
        }
    
        public long id { get; set; }
        public string nome { get; set; }
        public string descricao { get; set; }
        public float preco { get; set; }
        public long idLoja { get; set; }
    
        public virtual loja loja { get; set; }
        public virtual ICollection<produtofavorito> produtofavorito { get; set; }
    }
}
