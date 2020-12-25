using ModelLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataManipulation.DataFillers
{
    public class ComputerFiller
    {
        private Computer Computer = new Computer();

        public Computer FillComputers(List<Computer> sameComputers)
        {
            Computer.Id = sameComputers[0].Id;
            for (int i = 0; i < sameComputers.Count()
                           && (Computer.ManufacturerName == null
                           || Computer.GraphicsCardName == null
                           || Computer.GraphicsCardMemory == null
                           || Computer.RAM_type == null);                             
                                                                    i++)
            {
                if (Computer.ManufacturerName == null && sameComputers[i].ManufacturerName != null) Computer.ManufacturerName = sameComputers[i].ManufacturerName;
                if (Computer.GraphicsCardName == null && sameComputers[i].GraphicsCardName != null) Computer.GraphicsCardName = sameComputers[i].GraphicsCardName;
                if (Computer.GraphicsCardMemory == null && sameComputers[i].GraphicsCardMemory != null) Computer.GraphicsCardMemory = sameComputers[i].GraphicsCardMemory;
                if (Computer.RAM_type == null && sameComputers[i].RAM_type != null) Computer.RAM_type = sameComputers[i].RAM_type;
            }          
            return Computer;
        }
    }
}
