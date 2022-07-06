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
    
    public partial class AppCosmetologist
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AppCosmetologist()
        {
            this.AppUsers = new HashSet<AppUser>();
            this.SkinPlans = new HashSet<SkinPlan>();
        }
    
        public int cosmetologist_id { get; set; }
        public string cosmetologist_user_name { get; set; }
        public string cosmetologist_user_password { get; set; }
        public string cosmetologist_first_name { get; set; }
        public string cosmetologist_last_name { get; set; }
        public string cosmetologist_email { get; set; }
        public string cosmetologist_gender { get; set; }
        public Nullable<System.DateTime> cosmetologist_birth { get; set; }
        public Nullable<int> cosmetic_license_num { get; set; }
        public string cosmetic_businessName { get; set; }
        public string cosmetic_speciality { get; set; }
        public string cosmetic_address { get; set; }
        public string cosmetic_city { get; set; }
        public string cosmetic_status { get; set; }
        public string lng { get; set; }
        public string lat { get; set; }
        public Nullable<double> cosmetologist_rate { get; set; }
        public Nullable<int> cosmetologist_sumRate { get; set; }
        public Nullable<int> cosmetologist_numOfRates { get; set; }
        public string cosmetologist_phoneNumber { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AppUser> AppUsers { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SkinPlan> SkinPlans { get; set; }
    }
}
