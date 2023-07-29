using System;
using System.Collections.Generic;

namespace CoreApi.DatabaseModels;

public partial class Department
{
    public Guid? DepartmentGuid { get; set; }

    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
