using ModelLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataManipulation.DataFillers
{
    public class SmartphoneFiller
    {
        private Smartphone Smartphone = new Smartphone();

        public Smartphone FillSmartphones(List<Smartphone> sameSmartphones)
        {
            Smartphone.Id = sameSmartphones[0].Id;
            for (int i = 0; i < sameSmartphones.Count()
                           && (Smartphone.ManufacturerName == null
                           || Smartphone.BatteryStorage == 0
                           || Smartphone.Processor == null);
                                                                    i++)
            {
                if (Smartphone.ManufacturerName == null && sameSmartphones[i].ManufacturerName != null) Smartphone.ManufacturerName = sameSmartphones[i].ManufacturerName;
                if (Smartphone.BatteryStorage == 0 && sameSmartphones[i].BatteryStorage != 0) Smartphone.BatteryStorage = sameSmartphones[i].BatteryStorage;
                if (Smartphone.Processor == null && sameSmartphones[i].Processor != null) Smartphone.Processor = sameSmartphones[i].Processor;
            }
            return Smartphone;
        }
    }
}
