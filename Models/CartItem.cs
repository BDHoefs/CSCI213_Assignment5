﻿using System;
using System.Collections.Generic;

namespace Assignment5.Models;

public partial class CartItem
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int SongId { get; set; }

    public virtual Song Song { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
