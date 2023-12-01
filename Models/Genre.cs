using System;
using System.Collections.Generic;

namespace Assignment5.Models;

public partial class Genre
{
    public int GenreId { get; set; }

    public string GenreName { get; set; } = null!;

    public virtual ICollection<Song> Songs { get; set; } = new List<Song>();
}
