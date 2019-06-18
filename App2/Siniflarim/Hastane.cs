using System;
using System.Collections.Generic;
using System.Text;

namespace App2
{
    public class Hastane
    {
        public int Id { get; set; }

        public string Adi { get; set; }

        public string Sube { get; set; }

        

        public override string ToString()
        {
            return $"({Id}) {Adi},{Sube}";
        }
    }
}
