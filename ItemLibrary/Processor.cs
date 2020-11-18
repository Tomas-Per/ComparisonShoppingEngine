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
        public void SetName()
        {
            string name;
            if (Model.Contains("-")) name = Model.Substring(0, Model.IndexOf("-"));
            else name = Model.Substring(0, Model.IndexOf(Model.Split().Last()));
            if (name.Contains("Core") || name.Contains("Pentium")) name = "Intel " + name;
            else if (name.Contains("Ryzen") || name.Contains("Athlon")) name = "AMD " + name;
            Name = name;
        }
        
    }
}
