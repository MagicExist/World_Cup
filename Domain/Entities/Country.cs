using System;
using System.Collections.Generic;

namespace World_Cup.DataBase;

public partial class Country
{
    public int Id { get; set; }

    public string Country1 { get; set; } = null!;

    public string Entity { get; set; } = null!;

    public string? Flag { get; set; }

    public string? EntityLogo { get; set; }

    public virtual ICollection<ChampionShip> ChampionShips { get; set; } = new List<ChampionShip>();

    public virtual ICollection<City> Cities { get; set; } = new List<City>();

    public virtual ICollection<CountriesByGroup> CountriesByGroups { get; set; } = new List<CountriesByGroup>();

    public virtual ICollection<Match> MatchFirstCountries { get; set; } = new List<Match>();

    public virtual ICollection<Match> MatchSecondCountries { get; set; } = new List<Match>();
}
