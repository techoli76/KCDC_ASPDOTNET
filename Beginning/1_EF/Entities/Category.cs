using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SpyStore.Models.Entities.Base;

namespace SpyStore.Models.Entities
{
    [Table("Categories", Schema = "Store")]
    public class Category : EntityBase
    {
        [DataType(DataType.Text), MaxLength(50)]
        public string CategoryName { get; set; }

        [InverseProperty(nameof(Product.Category))]
        public virtual ICollection<Product> Products { get; set; }
            = new HashSet<Product>();

    }
}