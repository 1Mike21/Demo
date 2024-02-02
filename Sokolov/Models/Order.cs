using System;
using System.Collections.Generic;

namespace Sokolov.Models;

public partial class Order
{
    public int Id { get; set; }

    public DateTime Date { get; set; }

    public string Status { get; set; } = null!;

    public int UserId { get; set; }

    public string Place { get; set; } = null!;

    public int CountPerson { get; set; }

    public virtual ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();

    public virtual User User { get; set; } = null!;
}
