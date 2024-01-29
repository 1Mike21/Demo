using System;
using System.Collections.Generic;

namespace Sokolov.Models;

public partial class Shift
{
    public int Id { get; set; }

    public DateTime StartShift { get; set; }

    public DateTime EndShift { get; set; }

    public string StatusShift { get; set; } = null!;

    public virtual ICollection<ShiftUser> ShiftUsers { get; set; } = new List<ShiftUser>();
}
