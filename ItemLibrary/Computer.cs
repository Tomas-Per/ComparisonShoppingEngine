using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using static ItemLibrary.Categories;

namespace ItemLibrary
{
    public class Computer : Item
    {
        public Processor Processor { get; set; }

        [MaxLength(64)]
        public string GraphicsCardName { get; set; }

        [MaxLength(16)]
        public string GraphicsCardMemory { get; set; }

        [Required]
        public int RAM { get; set; }

        [MaxLength(16)]
        public string RAM_type { get; set; }

        [MaxLength(16)]
        public string Resolution { get; set; }
        public ComputerCategory ComputerCategory { get; set; }

        public static explicit operator Computer(List<Item> v)
        {
            throw new NotImplementedException();
        }

        [Required]
        public int StorageCapacity { get; set; }


        //builder pattern??
        public Computer()
        {
            //needed for csvHelper to work
        }



        //will remove this method in near future
        //find similar elements in a list
        public override List<Item> FindSimilar(List<Item> list)

        {
            IEnumerable<Computer> computers = list.Cast<Computer>().Where(comp => (comp != this && comp.RAM_type == this.RAM_type && comp.RAM == this.RAM) && 
                                                                (comp.Processor.Name == this.Processor.Name  
                                                                    || (comp.Price >= this.Price - 100 && comp.Price <= this.Price + 100) 
                                                                    || comp.StorageCapacity == this.StorageCapacity));
            return computers.Cast<Item>().ToList();
        }
        public override bool Equals(object obj)
        {
            if (obj == null || !this.GetType().Equals(obj.GetType())) return false;
            else
            {
                Computer comp = (Computer)obj;
                if (comp.ManufacturerName != this.ManufacturerName)
                {
                    if ((!comp.Name.Contains(this.ManufacturerName)) || (!this.Name.Contains(comp.ManufacturerName))) return false;
                }
                if (comp.Processor.Name == this.Processor.Name &&
                    comp.StorageCapacity == this.StorageCapacity &&
                     comp.RAM == this.RAM &&
                      comp.Resolution == comp.Resolution) return true;

                else return false;
            }
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
