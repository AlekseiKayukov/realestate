//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Недвижимость.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class Lands
    {
        public int Id { get; set; }
        public int CityId { get; set; }
        public int StreetId { get; set; }
        public Nullable<int> AddressHouse { get; set; }
        public Nullable<int> AddressNumber { get; set; }
        public int CoordinateLatitude { get; set; }
        public int CoordinateLongitude { get; set; }
        public double TotalArea { get; set; }
    
        public virtual Citys Citys { get; set; }
        public virtual Streets Streets { get; set; }
    }
}
