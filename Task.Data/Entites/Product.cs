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
        public string Name { get; set; }
        public double price { get; set; }
        public int Qty { get; set; }

        public int MianCategoryId { get; set; }
        [ForeignKey("MianCategoryId")]
        public virtual MainCategory MainCategory { get; set; }
        [ForeignKey("SubCategoryId")]
        public virtual SubCategory SubCategory { get; set; }
    }
}
