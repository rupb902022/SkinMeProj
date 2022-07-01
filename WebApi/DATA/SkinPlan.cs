//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DATA
{
    using System;
    using System.Collections.Generic;
    
    public partial class SkinPlan
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SkinPlan()
        {
            this.Products_for_plan = new HashSet<Products_for_plan>();
        }
    
        public int plan_id { get; set; }
        public Nullable<int> cosmetologist_id { get; set; }
        public Nullable<int> appUser_id { get; set; }
        public Nullable<int> prod_id { get; set; }
        public string plan_name { get; set; }
        public Nullable<System.DateTime> plan_date { get; set; }
        public string notes { get; set; }
    
        public virtual AppCosmetologist AppCosmetologist { get; set; }
        public virtual AppUser AppUser { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Products_for_plan> Products_for_plan { get; set; }
    }
}
