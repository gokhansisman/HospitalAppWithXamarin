using System;
using System.Collections.Generic;
using System.Text;

namespace App2
{
    public class Nesne
    {
        public int Id { get; set; }

        public int KoridorId { get; set; }

        public int OdaId { get; set; }

        public string Adi { get; set; }


        public override string ToString()
        {
            return $"({Id}) {KoridorId},{OdaId},{Adi}";
        }
    }
}
