using System;
using System.Collections.Generic;

namespace World_Cup.DataBase;

public partial class CountriesByGroup
{
    public int Id { get; set; }

    public int GroupId { get; set; }

    public int CountryId { get; set; }

    public virtual Country Country { get; set; } = null!;

    public virtual Group Group { get; set; } = null!;
}
