using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyComfort_Desktop
{
    internal class Item
    {
        public int ModelID { get; set; }
        public string ModelName { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public string MaterialName { get; set; }
        public string MaterialDescription { get; set; }
    }
}
