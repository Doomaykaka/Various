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
    
    public partial class Инвентарь
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Инвентарь()
        {
            this.Инвентарь1 = new HashSet<Инвентарь>();
            this.Обслуживающий_персонал = new HashSet<Обслуживающий_персонал>();
        }
    
        public string Наименование { get; set; }
        public int Номер_инвентаря { get; set; }
        public string Состояние { get; set; }
        public string Назначение { get; set; }
        public int Число_владельцев { get; set; }
        public int Стоимость { get; set; }
        public string Принцип_работы { get; set; }
        public int Номер_комплектного_инвентаря { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Инвентарь> Инвентарь1 { get; set; }
        public virtual Инвентарь Инвентарь2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Обслуживающий_персонал> Обслуживающий_персонал { get; set; }
    }
}
