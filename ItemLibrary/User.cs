using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLibrary
{
    public class User
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(32)]
        [Column(TypeName = "varchar(256)")]
        public string Username { get; set; }
        
        [Required]
        [MaxLength(64)]
        public string Email { get; set; }
        
        [Required]
        [MaxLength(128)]
        public string Password { get; set; }

        [MaxLength(128)]
        public string RecoveryPassword { get; set; }

        public DateTime RecoveryDate { get; set; }

        
    }
}
