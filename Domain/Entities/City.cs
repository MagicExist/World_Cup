using System;
using System.Collections.Generic;

namespace World_Cup.DataBase;

public partial class City
{
    public int Id { get; set; }

    public string City1 { get; set; } = null!;

    public int CountryId { get; set; }

    public virtual Country Country { get; set; } = null!;

    public virtual ICollection<Stadium> Stadia { get; set; } = new List<Stadium>();
}
