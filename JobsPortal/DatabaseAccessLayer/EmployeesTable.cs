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
    
    public partial class EmployeesTable
    {
        public int EmployeeID { get; set; }
        public int UserId { get; set; }
        public string EmployeeName { get; set; }
        public Nullable<System.DateTime> DOB { get; set; }
        public string Education { get; set; }
        public string WorkExperience { get; set; }
        public string Skills { get; set; }
        public string EmailAddress { get; set; }
        public string Gender { get; set; }
        public string Photo { get; set; }
        public string Qualification { get; set; }
        public string PermanentAddress { get; set; }
        public string JobReference { get; set; }
        public string Description { get; set; }
        public string ResumeName { get; set; }
        public byte[] ResumeData { get; set; }
        public string ResumeType { get; set; }
    
        public virtual UserTable UserTable { get; set; }
    }
}
