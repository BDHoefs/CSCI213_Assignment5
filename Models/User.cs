using System;
using System.Collections.Generic;

namespace Assignment5.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Name { get; set; } = null!;

    public string Type { get; set; } = null!;

    public virtual ICollection<TrackOwnership> TrackOwnerships { get; set; } = new List<TrackOwnership>();
}
