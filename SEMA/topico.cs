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
    
    public partial class topico
    {
        public int id { get; set; }
        public string descricao { get; set; }
        public int assuntoID { get; set; }
    
        public virtual assunto assunto { get; set; }
    }
}
