﻿using System.ComponentModel.DataAnnotations;

namespace ApiTest.Data
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }

        public ICollection<Item> Items { get; set; }

    }
}
