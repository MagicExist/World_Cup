using System;
using System.Collections.Generic;

namespace World_Cup.DataBase;

public partial class Group
{
    public int Id { get; set; }

    public string Group1 { get; set; } = null!;

    public int ChampionShipId { get; set; }

    public virtual ChampionShip ChampionShip { get; set; } = null!;

    public virtual ICollection<CountriesByGroup> CountriesByGroups { get; set; } = new List<CountriesByGroup>();
}
