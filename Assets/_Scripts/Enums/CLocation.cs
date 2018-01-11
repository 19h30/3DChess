using UnityEngine;
using System.Collections;

public struct CLocation  {
    public CLocation(int x, int y)
    { X = x; Y = y; }
    public int X;
    public int Y;

    public override string ToString()
    {
        return string.Format("[{0},{1}]",X,Y);
    }
}
