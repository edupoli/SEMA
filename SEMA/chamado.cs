//------------------------------------------------------------------------------
// <auto-generated>
//    O código foi gerado a partir de um modelo.
//
//    Alterações manuais neste arquivo podem provocar comportamento inesperado no aplicativo.
//    Alterações manuais neste arquivo serão substituídas se o código for gerado novamente.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SEMA
{
    using System;
    using System.Collections.Generic;
    
    public partial class chamado
    {
        public chamado()
        {
            this.historicoes = new HashSet<historico>();
        }
    
        public int id { get; set; }
        public string protocolo { get; set; }
        public string nome { get; set; }
        public string email { get; set; }
        public string telefone { get; set; }
        public string cpf { get; set; }
        public Nullable<int> assunto { get; set; }
        public Nullable<int> topico { get; set; }
        public Nullable<int> usuario_responsavel { get; set; }
        public string img { get; set; }
        public string status { get; set; }
        public Nullable<int> secretariaID { get; set; }
    
        public virtual assunto assunto1 { get; set; }
        public virtual ICollection<historico> historicoes { get; set; }
        public virtual usuario usuario { get; set; }
        public virtual secretaria secretaria { get; set; }
    }
}
