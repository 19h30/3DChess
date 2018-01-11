using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class P_King : BasePiece {

   

    public override void BeSelected()
    {
        playSelectedSound();

        List<CLocation> list = new List<CLocation>();
        //8 ô xung quanh nó
        CLocation c;
        
        //0 +1
        c = new CLocation(Location.X, Location.Y + 1);
        if (CHelper.CheckLocation(c))
            list.Add(c);

        //0 -1
        c = new CLocation(Location.X, Location.Y - 1);
        if (CHelper.CheckLocation(c))
            list.Add(c);
        
        //+1 0
        c = new CLocation(Location.X + 1, Location.Y );
        if (CHelper.CheckLocation(c))
            list.Add(c);

        //-1 0
        c = new CLocation(Location.X - 1, Location.Y );
        if (CHelper.CheckLocation(c))
            list.Add(c);

        //+1 +1
        c = new CLocation(Location.X+1, Location.Y + 1);
        if (CHelper.CheckLocation(c))
            list.Add(c);

        //+1 -1
        c = new CLocation(Location.X+1, Location.Y - 1);
        if (CHelper.CheckLocation(c))
            list.Add(c);

        //-1 +1
        c = new CLocation(Location.X-1, Location.Y + 1);
        if (CHelper.CheckLocation(c))
            list.Add(c);

        //-1 -1
        c = new CLocation(Location.X-1, Location.Y - 1);
        if (CHelper.CheckLocation(c))
            list.Add(c);

        foreach (var item in list)
        {
            Cell cell = ChessBoard.Current.Cells[item.X][item.Y];
            if (cell.CurrentPiece == null)
                _targetedCells.Add(cell);
            else if (cell.CurrentPiece.Player != BaseGameCTL.Current.CurrentPlayer)
                _targetedCells.Add(cell);
        }

        foreach (var item in _targetedCells)
            item.SetCellState(ECellState.TARGETED);
    }


    public override void BeAttackedBy(BasePiece enemy)
    {
        base.BeAttackedBy(enemy);
        BaseGameCTL.Current.GameOver(enemy.Player);
    }

   
}
