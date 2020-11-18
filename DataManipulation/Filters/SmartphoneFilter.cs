using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ItemLibrary;
using Parsing;

namespace DataManipulation.Filters
{
    public class SmartphoneFilter : Filter<Smartphone>
    {
        public SmartphoneFilter(List<Smartphone> items) : base(items)
        {

        }

        //Filters item list by the processor
        public List<Smartphone> FilterByProcessor(string processor)
        {
            List<Smartphone> result = _items.Where(item => (item.Processor).DeleteSpecialChars() == processor).ToList();
            return result;
        }

        //Filters item list by the screen diagonal
        public List<Smartphone> FilterByScreenDiagonal(double diagonal)
        {
            List<Smartphone> result = _items.Where(item => item.ScreenDiagonal == diagonal).ToList();
            return result;
        }

        //Filters item list by RAM amount
        public List<Smartphone> FilterByRAM(int RAM)
        {
            List<Smartphone> result = _items.Where(item => item.RAM == RAM).ToList();
            return result;
        }

        //Filters item list by storage amount
        public List<Smartphone> FilterByStorage(int storage)
        {
            List<Smartphone> result = _items.Where(item => item.Storage == storage).ToList();
            return result;
        }

        //Filters item list by camera type (quad, triple, dual, single)
        public List<Smartphone> FilterByCameraType(int cameraAmount)
        {
            List<Smartphone> result = _items.Where(item => item.BackCameraMP.Count == cameraAmount).ToList();
            return result;
        }

        //Filters item list by the megapixels if the phone has a single camera
        public List<Smartphone> FilterByCameraMP(int megapixels)
        {
            List<Smartphone> result = _items.Where(item => item.BackCameraMP.Count == 1 && item.BackCameraMP.First() == megapixels).ToList();
            return result;
        }
    }
}
