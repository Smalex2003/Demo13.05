//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PoprizhenokApp
{
    using System;
    using System.Collections.Generic;
    
    public partial class HistoryMovements
    {
        public int Id { get; set; }
        public Nullable<int> CardId { get; set; }
        public Nullable<int> Place { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<System.TimeSpan> Time { get; set; }
    
        public virtual NFCCards NFCCards { get; set; }
    }
}
