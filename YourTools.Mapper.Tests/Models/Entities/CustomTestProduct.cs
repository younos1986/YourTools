﻿namespace YourTools.Mapper.Tests.Models.Entities;

public class CustomTestProduct
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public CustomTestCategory? Category { get; set; }
}
