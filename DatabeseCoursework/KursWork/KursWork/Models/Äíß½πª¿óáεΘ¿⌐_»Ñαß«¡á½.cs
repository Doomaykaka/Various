//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KursWork.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Обслуживающий_персонал
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Обслуживающий_персонал()
        {
            this.Инвентарь = new HashSet<Инвентарь>();
        }
    
        public string ФИО { get; set; }
        public string Должность { get; set; }
        public int Договор { get; set; }
        public string Паспорт_работника { get; set; }
        public int ID_процесса_обработки { get; set; }
    
        public virtual Обработка Обработка { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Инвентарь> Инвентарь { get; set; }
    }
}
