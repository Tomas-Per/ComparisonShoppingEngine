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
            //FrontCameras = string.Join(',', FrontCameraMP.Select(x => x.ToString()).ToArray());
            //BackCameras = string.Join(',', BackCameraMP.Select(x => x.ToString()).ToArray());
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

        public override bool Equals(object obj)
        {
            if (obj == null || !this.GetType().Equals(obj.GetType())) return false;
            else
            {
                Smartphone phone = (Smartphone)obj;
                //check if the manufacturer is the same (if it isn't in manufacturer field, it should be in the name then)
                if (phone.ManufacturerName != this.ManufacturerName)
                {
                    if (((this.ManufacturerName != null && !phone.Name.Contains(this.ManufacturerName)))
                       && ((phone.Name != null && !this.Name.Contains(phone.ManufacturerName)))) return false;
                }

                //check if all mandatory fields are equal
                if (phone.Resolution !=  null && this.Resolution != null &&
                    phone.Storage == this.Storage &&
                     phone.RAM == this.RAM &&
                      (phone.Resolution.Contains(this.Resolution) || this.Resolution.Contains(phone.Resolution)) &&
                        (!phone.ShopName.Equals(this.ShopName))) return true;

                else return false;
            }
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
