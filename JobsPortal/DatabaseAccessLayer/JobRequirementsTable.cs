//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DatabaseAccessLayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class JobRequirementsTable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public JobRequirementsTable()
        {
            this.JobRequirementDetailsTables = new HashSet<JobRequirementDetailsTable>();
        }
    
        public int JobRequirementID { get; set; }
        public string JobRequirement { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<JobRequirementDetailsTable> JobRequirementDetailsTables { get; set; }
    }
}
