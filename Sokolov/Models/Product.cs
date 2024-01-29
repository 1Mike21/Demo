using System;
using System.Collections.Generic;

namespace Sokolov.Models;

public partial class Product
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public float Price { get; set; }

    public int Count { get; set; }

    public virtual ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
}
