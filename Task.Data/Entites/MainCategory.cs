using Data.Entites;
using System.ComponentModel.DataAnnotations;

namespace  Entites
{
    public class MainCategory:BaseModel
    {
        [Key]
        public int Id { get; set; }
        public int Name { get; set; }
    }
}
