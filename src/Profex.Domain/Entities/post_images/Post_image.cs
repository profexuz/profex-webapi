﻿namespace Profex.Domain.Entities.post_images;

public class Post_image : Auditable
{
    public long PostId { get; set; }
    public string ImagePath { get; set; } = string.Empty;
    //public List<string> ImagePath { get; set; } = new List<string>();
}
