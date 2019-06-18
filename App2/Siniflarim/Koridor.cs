using System;
using System.Collections.Generic;
using System.Text;

namespace App2
{
    public class Koridor
    {
        public int Id { get; set; }

        public int Numara { get; set; }

        public int HastaneId { get; set; }

        public override string ToString()
        {
            return $"({Id}) ,{Numara},{HastaneId}";
        }
    }
}
