using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemLibrary
{
    public class Component
    {
        public int Id { get; set; }

        [MaxLength(32)]
        public string Name { get; set; }
    }
}
