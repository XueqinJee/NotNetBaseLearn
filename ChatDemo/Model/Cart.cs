using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatDemo.Model
{
    class Cart
    {
        public string MenuId { get; set; }
        public int Count { get; set; }
        public float Price { get; set; }
        public float SumPrice { get; set; }

    }
}
