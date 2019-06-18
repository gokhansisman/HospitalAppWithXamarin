using System;
using System.Collections.Generic;
using System.Text;

namespace App2
{
    public class Kullanici
    {
        public int Id { get; set; }

        public string Adi { get; set; }

        public string Sifre { get; set; }


        public override string ToString()
        {
            return $"({Id}) {Adi},{Sifre}";
        }
    }
}
