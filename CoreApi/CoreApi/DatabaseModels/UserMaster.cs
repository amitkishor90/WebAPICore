using System;
using System.Collections.Generic;

namespace CoreApi.DatabaseModels;

public partial class UserMaster
{
    public int Id { get; set; }

    public int? EmpId { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public DateTime? LastLogin { get; set; }

    public DateTime? LastLogout { get; set; }

    public virtual Employee? Emp { get; set; }
}
