﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace doan.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class QLGIAIBONGDAEntities1 : DbContext
    {
        public QLGIAIBONGDAEntities1()
            : base("name=QLGIAIBONGDAEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<BANTHANG> BANTHANG { get; set; }
        public virtual DbSet<CAUTHU> CAUTHU { get; set; }
        public virtual DbSet<DOIBONG> DOIBONG { get; set; }
        public virtual DbSet<TRANDAU> TRANDAU { get; set; }
        public virtual DbSet<VONGDAU> VONGDAU { get; set; }
    }
}
