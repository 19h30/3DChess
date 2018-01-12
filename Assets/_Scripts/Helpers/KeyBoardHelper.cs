using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBoardHelper{
   
    public static void Update(Cell newCell)
    {
        CLocation c;
        //Space to Pause
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space");
        }
        // BaseGameCTL.Current.SwitchTurn();

        if (Input.GetKeyDown(KeyCode.W))
        {
            c = new CLocation(newCell.Location.X, newCell.Location.Y + 1);
            if (CHelper.CheckLocation(c))
            {
                newCell.SetLocation(c);
            }
            Debug.Log("W");
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            c = new CLocation(newCell.Location.X, newCell.Location.Y - 1);
            if (CHelper.CheckLocation(c))
            {
                newCell.SetLocation(c);
            }
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            c = new CLocation(newCell.Location.X - 1, newCell.Location.Y );
            if (CHelper.CheckLocation(c))
            {
                newCell.SetLocation(c);
            }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            c = new CLocation(newCell.Location.X + 1, newCell.Location.Y);
            if (CHelper.CheckLocation(c))
            {
                newCell.SetLocation(c);
            }
        }

    }


}
