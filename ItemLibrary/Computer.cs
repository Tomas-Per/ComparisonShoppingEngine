﻿using System;
using System.Collections.Generic;
using System.Linq;
using static ItemLibrary.Categories;

namespace ItemLibrary
{
    public class Computer : Item
    {
        public string ProcessorName { get; set; }
        public string GraphicsCardName { get; set; }
        public string GraphicsCardMemory { get; set; }
        public int RAM { get; set; }
        public string RAM_type { get; set; }
        public string Resolution { get; set; }
        public ComputerCategory ComputerCategory { get; set; }

        public static explicit operator Computer(List<Item> v)
        {
            throw new NotImplementedException();
        }

        public int StorageCapacity { get; set; }


        //builder pattern??
        public Computer()
        {
            //needed for csvHelper to work
        }
       
        //find similar elements in a list
        public override List<Item> FindSimilar(List<Item> list)

        {
            IEnumerable<Computer> computers = list.Cast<Computer>().Where(comp => comp.ProcessorName == this.ProcessorName
                                                                || (comp.Price >= this.Price - 100 && comp.Price <= this.Price + 100) && comp.Name != this.Name);
            return computers.Cast<Item>().ToList();
        }

    }
}
