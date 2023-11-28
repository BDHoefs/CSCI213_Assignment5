using System;
using System.Collections.Generic;

namespace Assignment5.Models;

public partial class Song
{
    public int SongId { get; set; }

    public string Name { get; set; } = null!;

    public int Length { get; set; }

    public decimal Price { get; set; }

    public int MusicianId { get; set; }

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    public virtual Musician Musician { get; set; } = null!;
}
