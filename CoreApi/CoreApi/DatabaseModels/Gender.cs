using System;
using System.Collections.Generic;

namespace CoreApi.DatabaseModels;

public partial class Gender
{
    public Guid? GenderGuid { get; set; }

    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
