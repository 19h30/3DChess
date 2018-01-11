using UnityEngine;
using System.Collections;

public class CHelper  {
    
    /// <summary>
    /// Kiểm tra xem tọa độ đó có thỏa mãn không [0,7]
    /// </summary>s
    public static bool CheckLocation(CLocation c)
    {
        if (c.X >= 0 && c.X <= 7 && c.Y >= 0 && c.Y <= 7)
            return true;
        return false;
    }
	
       
}
