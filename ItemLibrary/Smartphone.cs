using System;
using System.Collections.Generic;
using System.Text;

namespace ItemLibrary
{
    public class Smartphone : Item
    {
        public string FrontCamera { get; set; }
        public string EndCamera { get; set; }
        public double ScreenDiagonal { get; set; }
        public int Storage { get; set; }
        public int RAM { get; set; }
        public string Processor { get; set; }
        public string DisplaySize { get; set; }


        public Smartphone ()
        {
        }

        //will remove this method in near future
        public override List<Item> FindSimilar(List<Item> list)
        {
            throw new NotImplementedException();
        }
    }
}
