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
    
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            this.SkinPlans = new HashSet<SkinPlan>();
        }
    
        public int prod_id { get; set; }
        public string prod_name { get; set; }
        public string prod_type { get; set; }
        public string prod_company { get; set; }
        public string prod_description { get; set; }
        public string prod_manual { get; set; }
        public Nullable<double> prod_rate { get; set; }
        public string prod_sizeType { get; set; }
        public Nullable<int> prod_size { get; set; }
        public string prod_status { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SkinPlan> SkinPlans { get; set; }
    }
}
