using System;
using System.Collections.Generic;

namespace World_Cup.DataBase;

public partial class Match
{
    public int Id { get; set; }

    public DateOnly? Date { get; set; }

    public int FirstCountryGoals { get; set; }

    public int SecondCountryGoals { get; set; }

    public int FirstCountryId { get; set; }

    public int SecondCountryId { get; set; }

    public int StadiumId { get; set; }

    public int PhaseId { get; set; }

    public int ChampionShipId { get; set; }

    public virtual ChampionShip ChampionShip { get; set; } = null!;

    public virtual Country FirstCountry { get; set; } = null!;

    public virtual Phase Phase { get; set; } = null!;

    public virtual Country SecondCountry { get; set; } = null!;

    public virtual Stadium Stadium { get; set; } = null!;
}
