using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLibrary;
using Parsing;

namespace DataManipulation.Filters
{
    public class SmartphoneFilter : Filter<Smartphone>
    {

        //Filters item filter by the processor
        public void FilterByProcessor(string processor, List<Smartphone> items, FilterList filter)
        {
            filter(items.Where(item => (item.Processor).DeleteSpecialChars() == processor).ToList());
        }

        //Filters item filter by the screen diagonal
        public void FilterByScreenDiagonal(double diagonal, List<Smartphone> items, FilterList filter)
        {
            filter(items.Where(item => item.ScreenDiagonal == diagonal).ToList());
        }

        //Filters item filter by RAM amount
        public void FilterByRAM(int RAM, List<Smartphone> items, FilterList filter)
        {
            filter(items.Where(item => item.RAM == RAM).ToList());
        }

        //Filters item filter by storage amount
        public void FilterByStorage(int storage, List<Smartphone> items, FilterList filter)
        {
            filter(items.Where(item => item.Storage == storage).ToList());
        }

        //Filters item filter by camera type (quad, triple, dual, single)
        public void FilterByCameraType(int cameraAmount, List<Smartphone> items, FilterList filter)
        {
            filter(items.Where(item => item.BackCameraMP.Count == cameraAmount).ToList());
        }

        //Filters item filter by the megapixels if the phone has a single camera
        public void FilterByCameraMP(int megapixels, List<Smartphone> items, FilterList filter)
        {
            filter(items.Where(item => item.BackCameraMP.Count == 1 && item.BackCameraMP.First() == megapixels).ToList());
        }
    }
}
