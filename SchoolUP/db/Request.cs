//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SchoolUP.db
{
    using System;
    using System.Collections.Generic;
    
    public partial class Request
    {
        public int Id { get; set; }
        public Nullable<int> Code { get; set; }
        public string Number { get; set; }
    
        public virtual Specialities Specialities { get; set; }
    }
}
