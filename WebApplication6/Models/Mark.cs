
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
    
public partial class Mark
{

    public int Id { get; set; }

    public Nullable<int> ProjectId { get; set; }

    public Nullable<int> ExamId { get; set; }

    public Nullable<int> QuestionId { get; set; }

    public Nullable<int> StudentId { get; set; }

    public double Marks { get; set; }



    public virtual Exam Exam { get; set; }

    public virtual Project Project { get; set; }

    public virtual Question Question { get; set; }

    public virtual Student Student { get; set; }

}

}
