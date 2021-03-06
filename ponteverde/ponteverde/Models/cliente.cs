//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ponteverde.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class cliente
    {
        public cliente()
        {
            this.lojafavorita = new HashSet<lojafavorita>();
            this.produtofavorito = new HashSet<produtofavorito>();
        }
    
        public long id { get; set; }
        public long idUsername { get; set; }
        public string nome { get; set; }
        public string fotowall { get; set; }
        public string fotoperfil { get; set; }
        public string cep { get; set; }
        public string logradouro { get; set; }
        public string numero { get; set; }
        public Nullable<decimal> latitude { get; set; }
        public Nullable<decimal> longitude { get; set; }
        public long idBairro { get; set; }
        public string statusPublico { get; set; }
    
        public virtual bairro bairro { get; set; }
        public virtual ICollection<lojafavorita> lojafavorita { get; set; }
        public virtual usuario usuario { get; set; }
        public virtual ICollection<produtofavorito> produtofavorito { get; set; }
    }
}
