using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static ItemLibrary.Categories;

namespace ItemLibrary
{
    public abstract class Item
    {
        //not needed at the moment
        //public ulong ItemCode { get; set; }
        public int Id { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        [MaxLength(256)]
        public string Name { get; set; }

        [MaxLength(32)]
        public string ManufacturerName { get; set; }

        [Required]
        [MaxLength(256)]
        [Column(TypeName = "varchar(256)")]
        public string ItemURL { get; set; }

        [MaxLength(16)]
        public string ShopName { get; set; }  

        [MaxLength(256)]
        public string ImageLink { get; set; }
        public ItemCategory ItemCategory { get; set; }

        //will remove this method in near future
        public abstract List<Item> FindSimilar(List<Item> list);

    }
}
