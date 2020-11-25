using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace ItemLibrary
{
    public class Smartphone : Item
    {
        [NotMapped]
        public List<int> FrontCameraMP { get; set; }

        [NotMapped]
        public List<int> BackCameraMP { get; set; }

        [MaxLength(32)]
        public string FrontCameras { get; set; }

        [MaxLength(32)]
        public string BackCameras { get; set; }

        public double ScreenDiagonal { get; set; }

        public int Storage { get; set; }

        public int RAM { get; set; }

        [MaxLength(64)]
        public string Processor { get; set; }

        [MaxLength(16)]
        public string Resolution { get; set; }
        public int BatteryStorage { get; set; }


        public Smartphone ()
        {
            FrontCameras = string.Join(',', FrontCameraMP);
            BackCameras = string.Join(',', BackCameraMP);
            ModifyDate = DateTime.Now;
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
