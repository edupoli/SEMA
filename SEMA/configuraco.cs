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
    
    public partial class configuraco
    {
        public int id { get; set; }
        public Nullable<int> secretariaID { get; set; }
        public string logo { get; set; }
        public string bckColorMenu { get; set; }
        public string onHoverbckColorMenu { get; set; }
        public string textColorMenu { get; set; }
        public string onHovertexColorMenu { get; set; }
        public string bckColorSbMenu { get; set; }
        public string onHoverbckColorSbMenu { get; set; }
        public string textColorSbMenu { get; set; }
        public string onHovertextColorSbMenu { get; set; }
        public string bckColorNavbar { get; set; }
        public string smtp { get; set; }
        public string porta { get; set; }
        public string email { get; set; }
        public string senhaEmail { get; set; }
        public string nomeRemetente { get; set; }
        public string assunto { get; set; }
        public string bodyEmailAuto { get; set; }
        public string bodyEmailResposta { get; set; }
    
        public virtual secretaria secretaria { get; set; }
    }
}
