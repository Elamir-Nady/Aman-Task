using Data.Entites;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace  Entites
{
    public class SubCategory: BaseModel
    {
        [Key]
        public int Id { get; set; }
        [Required]

        public string Name { get; set; }
        [Required]

        public int MianCategoryId { get; set; }
        [ForeignKey("MianCategoryId")]
        public virtual MainCategory MainCategory { get; set; }
        public virtual List<Product> Products { get; set; }

    }
}
