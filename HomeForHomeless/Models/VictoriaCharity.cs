//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HomeForHomeless.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class VictoriaCharity
    {
        public int charity_Id { get; set; }
        public string charity_name { get; set; }
        public string Registration_Status { get; set; }
        public string address_line_1 { get; set; }
        public string address_line_2 { get; set; }
        public string town_city { get; set; }
        public string state { get; set; }
        public Nullable<int> postcode { get; set; }
        public string country { get; set; }
        public string charity_size { get; set; }
        public string main_activity { get; set; }
        public string main_beneficiaries { get; set; }
    }
}
