using System;
using System.Collections.Generic;

namespace Assignment5.Models;

public partial class TrackOwnership
{
    public int OwnershipId { get; set; }

    public int UserId { get; set; }

    public int TrackId { get; set; }

    public virtual Track Track { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
