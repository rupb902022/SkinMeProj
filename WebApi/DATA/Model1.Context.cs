﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class bgroup90_test2Entities2 : DbContext
    {
        public bgroup90_test2Entities2()
            : base("name=bgroup90_test2Entities2")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ApprovedCo> ApprovedCos { get; set; }
        public virtual DbSet<AppUser> AppUsers { get; set; }
        public virtual DbSet<CarePlan> CarePlans { get; set; }
        public virtual DbSet<Product> Products { get; set; }
    }
}
