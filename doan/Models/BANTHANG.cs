//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class BANTHANG
    {
        public string MaBT { get; set; }
        public string MaCT { get; set; }
        public string MaTD { get; set; }
    
        public virtual CAUTHU CAUTHU { get; set; }
        public virtual TRANDAU TRANDAU { get; set; }
    }
}
