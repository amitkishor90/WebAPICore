using System;
using System.Collections.Generic;

namespace CoreApi.DatabaseModels;

public partial class Employee
{
    public int Id { get; set; }

    public Guid? Guid { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Emailid { get; set; }

    public string? PenCardNo { get; set; }

    public decimal? Salary { get; set; }

    public int? GenderId { get; set; }

    public int? DepartmentId { get; set; }

    public string? Address { get; set; }

    public string? Role { get; set; }

    public DateTime? DateIns { get; set; }

    public DateTime? DateUpdate { get; set; }

    public virtual Department? Department { get; set; }

    public virtual Gender? Gender { get; set; }
}
