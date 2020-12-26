using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using ModelLibrary.Exceptions;

namespace ModelLibrary
{
    public class Processor : Component
    {

        [MaxLength(32)]
        public string Model { get; set; }
        public  int Cache { get; set; }
        public int MinCores { get; set; }

        //sets Processor type/name according to it's model
        public void SetName(string processorModel)
        {
            string name;
            if (processorModel.Contains("-")) name = processorModel.Substring(0, processorModel.IndexOf("-"));
            else
            { var words = processorModel.Split();
                if (words.Last() != "")
                    name = processorModel.Substring(0, processorModel.IndexOf(processorModel.Split(' ').Last()));
                else name = processorModel.Substring(0, processorModel.IndexOf(words[words.Count() - 2]));
            }

            this.Name = name;

            if (name.Length <= 3) throw new ProcessorInvalidNameException("Processor has invalid name");
         }
        
    }
}
