using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exercise01.Models
{
    [Table("categories")]
    public class Category : BaseEntity
    {
        [Key]
        [Column("category_id")] 
        public int CategoryId { get; set; }

        [Column("sub_category_id")]
        [ForeignKey("SubCategoryId")]
        public int? SubCategoryId { get; set; }

        [Column("category_title")]
        public string CategoryTitle { get; set; }

        [Column("image_url")]
        public string ImageUrl { get; set; }

        [ForeignKey("SubCategoryId")]
        public virtual Category SubCategory { get; set; }

        [NotMapped]
        public virtual ICollection<Product> Products { get; set; }
    }
}
