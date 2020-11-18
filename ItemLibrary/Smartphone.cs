using System;
using System.Collections.Generic;
using System.Text;

namespace ItemLibrary
{
    public class Smartphone : Item
    {
        public List<int> FrontCameraMP { get; set; }
        public List<int> BackCameraMP { get; set; }
        public double ScreenDiagonal { get; set; }
        public int Storage { get; set; }
        public int RAM { get; set; }
        public string Processor { get; set; }
        public string Resolution { get; set; }
        public int BatteryStorage { get; set; }


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
