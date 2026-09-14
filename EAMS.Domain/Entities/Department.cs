using System;
using System.Collections.Generic;
using System.Text;
using EAMS.Domain.Common;

namespace EAMS.Domain.Entities;

public class Department : BaseSoftDeleteEntity
{
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    //public ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
