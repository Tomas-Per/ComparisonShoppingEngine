using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ModelLibrary
{
    public class Review
    {
        public int Id { get; set; }
        
        [MaxLength(280)]
        public string Message { get; set; }
        
        [Required]
        public double Score { get; set; }
        public Item Item { get; set; }
        public User User { get; set; }
    }
}
