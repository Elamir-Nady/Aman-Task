using Data.Entites;
using System.ComponentModel.DataAnnotations;

namespace  Entites
{
    public class MainCategory:BaseModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
