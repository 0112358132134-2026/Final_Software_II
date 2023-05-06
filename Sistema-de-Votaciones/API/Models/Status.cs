using System;
using System.Collections.Generic;

namespace API.Models;

public partial class Status
{
    public int Id { get; set; }

    public string TableName { get; set; } = null!;

    public bool Status1 { get; set; }
}
