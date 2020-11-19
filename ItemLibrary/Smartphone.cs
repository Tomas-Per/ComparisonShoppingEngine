using System;
using System.Collections.Generic;
using System.Linq;
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
        public string DisplaySize { get; set; }
        public int BatteryStorage { get; set; }


        public Smartphone ()
        {
        }

        //find similar elements in a list
        public override List<Item> FindSimilar(List<Item> list)

        {
            IEnumerable<Smartphone> phones = list.Cast<Smartphone>().Where(phone => phone != this && phone.RAM == this.RAM &&
                                                                                    phone.Price >= this.Price - 100 && phone.Price <= this.Price + 100 &&
                                                                                    phone.Storage == this.Storage &&
                                                                                    (phone.BackCameraMP.Count == this.BackCameraMP.Count + 1 ||
                                                                                    phone.BackCameraMP.Count == this.BackCameraMP.Count - 1));
            return phones.Cast<Item>().ToList();
        }
    }
}
