using System;
using System.Collections.Generic;

namespace Magato.Api.Models
{
    public class BlogPost
    {
        public int Id
        {
            get; set;
        }
        public string Title { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string? Summary
        {
            get; set;
        }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt
        {
            get; set;
        }
        public bool Published { get; set; } = false;
        public List<string> ImageUrls { get; set; } = new();
    }
}
