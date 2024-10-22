using System;
using System.Collections.Generic;

namespace World_Cup.DataBase;

public partial class ChampionShip
{
    public int Id { get; set; }

    public string ChampionShip1 { get; set; } = null!;

    public short Year { get; set; }

    public int CountryId { get; set; }

    public virtual Country Country { get; set; } = null!;

    public virtual ICollection<Group> Groups { get; set; } = new List<Group>();

    public virtual ICollection<Match> Matches { get; set; } = new List<Match>();
}
