using Data.Entites;
using Entites;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entites
{
    public class Product: BaseModel
    {
        [Key]
        public int Id { get; set; }
        [Required]

        public string Name { get; set; }
        [Required]

        public double price { get; set; }
        [Required]

        public int Qty { get; set; }
        [ForeignKey("SubCategoryId")]
        public virtual SubCategory SubCategory { get; set; }
        [Required]

        public int SubCategoryId { get; set; }
        [Required]

        public int MianCategoryId { get; set; }
        [ForeignKey("MianCategoryId")]
        public virtual MainCategory MainCategory { get; set; }
        
    }
}
