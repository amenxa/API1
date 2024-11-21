using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiTest.Data
{
    public class Item
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int price { get; set; }

        public string? Description { get; set; }

        [ForeignKey("category")]
        public int categoryId { get; set; }

        Category category { get; set; }
        
    }
}
