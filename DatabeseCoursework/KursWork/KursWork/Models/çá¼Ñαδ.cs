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
    
    public partial class Замеры
    {
        public int Средняя_температура_на_участке { get; set; }
        public int Давление { get; set; }
        public int Влажность_участка { get; set; }
        public string Освещенность_участка { get; set; }
        public int Пригодность_параметров { get; set; }
        public System.DateTime Дата_проведения { get; set; }
        public int Номер_замера { get; set; }
        public int Номер_участка { get; set; }
    
        public virtual Участок Участок { get; set; }
    }
}
