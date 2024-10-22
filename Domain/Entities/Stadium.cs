using System;
using System.Collections.Generic;

namespace World_Cup.DataBase;

public partial class Stadium
{
    public int Id { get; set; }

    public string Stadium1 { get; set; } = null!;

    public int Capacity { get; set; }

    public string? Photo { get; set; }

    public int CityId { get; set; }

    public virtual City City { get; set; } = null!;

    public virtual ICollection<Match> Matches { get; set; } = new List<Match>();
}
