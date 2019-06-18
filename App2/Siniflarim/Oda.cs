using System;
using System.Collections.Generic;
using System.Text;

namespace App2
{
    public class Oda
    {
        public int Id { get; set; }

        public int KoridorId { get; set; }

        public int Numara { get; set; }


        

        public override string ToString()
        {
            return $"({Id}) {KoridorId},{Numara}";
        }
    }
}
