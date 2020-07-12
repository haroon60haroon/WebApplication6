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
    
    public partial class Comment
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Comment()
        {
            this.Statements = new HashSet<Statement>();
        }
    
        public int Id { get; set; }
        public Nullable<int> InstructorId { get; set; }
        public Nullable<int> ProjectExamId { get; set; }
        public string SourcePath { get; set; }
        public string Reason { get; set; }
    
        public virtual ProjectExam ProjectExam { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Statement> Statements { get; set; }
        public virtual Instructor Instructor { get; set; }
    }
}