using System;
using System.Collections.Generic;
using System.Text;

namespace App2
{
    public static class EkÖzellikler
    {
       
            public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
            {
                int diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
                return dt.AddDays(-1 * diff).Date;
            }
        
    }
}
