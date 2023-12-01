using System;
using System.Collections.Generic;

namespace Assignment5.Models;

public partial class Musician
{
    public int MusicianId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Song> Songs { get; set; } = new List<Song>();
}
