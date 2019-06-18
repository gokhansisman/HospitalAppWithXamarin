using System;
using System.Collections.Generic;
using System.Text;

namespace App2
{
    public static class EnteredUserInfos
    {
        public static Kullanici Current { get; set; }
        public static Doktor SuankiDoktor { get; set; }
        public static Hemsire SuankiHemsire { get; set; }

        public static bool IsNull(this Kullanici kullanici)
        {
            if (kullanici == null)
                return true;

            return false;
        }
    }
}
