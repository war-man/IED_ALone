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
    
    public partial class T_LinePositionDetail
    {
        public int Id { get; set; }
        public int Line_PositionId { get; set; }
        public int TechProVerDe_Id { get; set; }
        public int OrderIndex { get; set; }
        public double DevisionPercent { get; set; }
        public double NumberOfLabor { get; set; }
        public string Note { get; set; }
        public bool IsPass { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedUser { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> UpdatedUser { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<int> DeletedUser { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
    
        public virtual T_LinePosition T_LinePosition { get; set; }
        public virtual T_TechProcessVersionDetail T_TechProcessVersionDetail { get; set; }
    }
}
