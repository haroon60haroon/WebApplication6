//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication6.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Assessment
    {
        public int Id { get; set; }
        public Nullable<int> ProjectExamId { get; set; }
        public Nullable<int> QuestionId { get; set; }
        public Nullable<int> StudentId { get; set; }
    
        public virtual ProjectExam ProjectExam { get; set; }
        public virtual Question Question { get; set; }
        public virtual Student Student { get; set; }
    }
}
