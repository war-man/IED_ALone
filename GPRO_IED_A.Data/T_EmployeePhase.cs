//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GPRO_IED_A.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class T_EmployeePhase
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int CommodityId { get; set; }
        public int LineId { get; set; }
        public int EmployeeId { get; set; }
        public string ListEmployeePhase { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedUser { get; set; }
        public Nullable<int> DeletedUser { get; set; }
        public Nullable<int> UpdatedUser { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<bool> Estatus { get; set; }
    }
}