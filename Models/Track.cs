using System;
using System.Collections.Generic;

namespace Assignment5.Models;

public partial class Track
{
    public int TrackId { get; set; }

    public string Title { get; set; } = null!;

    public string Artist { get; set; } = null!;

    public int Length { get; set; }

    public decimal Price { get; set; }

    public virtual ICollection<TrackOwnership> TrackOwnerships { get; set; } = new List<TrackOwnership>();
}
