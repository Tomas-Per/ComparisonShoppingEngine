using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static ModelLibrary.Categories;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ModelLibrary
{
    public class Item
    {
       
        public int Id { get; set; }

        [Required]
        public int ItemCode { get; set; }

        [Required]
        public DateTime ModifyDate { get; set; }

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
        public virtual List<Item> FindSimilar(List<Item> list)
        {
            return list;
        }

    }
}
