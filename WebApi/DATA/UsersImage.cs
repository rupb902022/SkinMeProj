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
    
    public partial class UsersImage
    {
        public int img_id { get; set; }
        public string Imgurl { get; set; }
        public Nullable<int> appUser_id { get; set; }
        public Nullable<System.DateTime> upload_date { get; set; }
    
        public virtual AppUser AppUser { get; set; }
    }
}
