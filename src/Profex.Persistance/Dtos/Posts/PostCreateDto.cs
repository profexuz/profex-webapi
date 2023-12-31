﻿namespace Profex.Persistance.Dtos.Posts;

public class PostCreateDto
{
    public long CategoryId { get; set; }
    //public long UserId { get; set; }
    public string Title { get; set; } = string.Empty;
    public double Price { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Region { get; set; } = string.Empty;
    public string District { get; set; } = string.Empty;
    public double Latidute { get; set; }
    public double Longitude { get; set; }
    public string PhoneNumber { get; set; } = string.Empty;
    public bool Status { get; set; } = true;
    public string Address { get; set; } = String.Empty;
    public string JobTime { get; set; } = String.Empty;
}
