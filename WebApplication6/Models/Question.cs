
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
    
public partial class Question
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public Question()
    {

        this.Assessments = new HashSet<Assessment>();

        this.Marks = new HashSet<Mark>();

    }


    public int Id { get; set; }

    public string Title { get; set; }

    public int CLOId { get; set; }

    public int ExamId { get; set; }

    public int TermId { get; set; }

    public string IsActive { get; set; }



    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<Assessment> Assessments { get; set; }

    public virtual CLO CLO { get; set; }

    public virtual Exam Exam { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<Mark> Marks { get; set; }

    public virtual Term Term { get; set; }

}

}
