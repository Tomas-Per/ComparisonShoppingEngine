using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ItemLibrary
{
    public class Processor
    {
        public int Id { get; set; }

        [MaxLength(32)]
        public string Name { get; set; }

        [MaxLength(32)]
        public string Model { get; set; }
        public  int Cache { get; set; }
        public int MinCores { get; set; }

        //sets Processor type/name according to it's model
        public void SetName(string processorModel)
        {
            string name;
            if (processorModel.Contains("-")) name = processorModel.Substring(0, processorModel.IndexOf("-"));
            else name = processorModel.Substring(0, processorModel.IndexOf(processorModel.Split().Last()));
            Name = name;
        }
        
    }
}
