using System;
using System.Collections.Generic;
using System.Text;

namespace App2
{
    public class Hemsire
    {
        public int Id { get; set; }

        public string Adı { get; set; }

        public int HastaneId { get; set; }



        public override string ToString()
        {
            return $"({Id}) {Adı},{HastaneId}";
        }


    }
}
