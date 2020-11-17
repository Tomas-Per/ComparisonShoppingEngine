using System;
using System.ComponentModel.DataAnnotations;

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
        
    }
}
