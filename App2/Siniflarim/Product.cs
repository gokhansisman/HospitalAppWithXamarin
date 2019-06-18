using System;
using System.Collections.Generic;
using System.Text;

namespace App2
{
   public  class Product
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public double Price { get; set; }

        public override string ToString()
        {
            return $"({Id}) {Title},{Price}";
        }
    }
}
