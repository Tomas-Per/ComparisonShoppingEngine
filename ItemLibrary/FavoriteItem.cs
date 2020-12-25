using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLibrary
{
    public class FavoriteItem
    {
        public int Id { get; set; }
        public Item Item { get; set; }
        public User User { get; set; }
    }
}
