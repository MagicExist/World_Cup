using System;
using System.Collections.Generic;

namespace World_Cup.DataBase;

public partial class Phase
{
    public int Id { get; set; }

    public string Phase1 { get; set; } = null!;

    public virtual ICollection<Match> Matches { get; set; } = new List<Match>();
}
