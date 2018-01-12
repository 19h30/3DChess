using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class P_Knight : BasePiece {

  
    public override void BeSelected()
    {
        playSelectedSound();

        //Xác định 8 vị trí mà mã có thể tới
        //1. Nếu trông => TARGETED
        //2. Nếu ko trống + là quân địch => TARGETED
        List<CLocation> locs = getTargetableLocation();

        foreach (var c in locs)
        {
            Cell cell = ChessBoard.Current.Cells[c.X][c.Y];
            if (cell.CurrentPiece == null)
                _targetedCells.Add(cell);
            else if (cell.CurrentPiece.Player != BaseGameCTL.Current.CurrentPlayer)
                _targetedCells.Add(cell);
        }

        foreach (var item in _targetedCells)
            item.SetCellState(ECellState.TARGETED);
    }

    private List<CLocation> getTargetableLocation()
    {
        List<CLocation> list = new List<CLocation>();

        CLocation c;
        //1
        c = new CLocation(this.Location.X + 2, this.Location.Y - 1);
        if (CHelper.CheckLocation(c))
            list.Add(c);

        //2
        c = new CLocation(this.Location.X + 2, this.Location.Y + 1);
        if (CHelper.CheckLocation(c))
            list.Add(c);

        //3
        c = new CLocation(this.Location.X - 2, this.Location.Y - 1);
        if (CHelper.CheckLocation(c))
            list.Add(c);

        //4
        c = new CLocation(this.Location.X - 2, this.Location.Y + 1);
        if (CHelper.CheckLocation(c))
            list.Add(c);

        //5
        c = new CLocation(this.Location.X + 1, this.Location.Y + 2);
        if (CHelper.CheckLocation(c))
            list.Add(c);

        //6
        c = new CLocation(this.Location.X - 1, this.Location.Y + 2);
        if (CHelper.CheckLocation(c))
            list.Add(c);

        //7
        c = new CLocation(this.Location.X + 1, this.Location.Y - 2);
        if (CHelper.CheckLocation(c))
            list.Add(c);

        //8
        c = new CLocation(this.Location.X - 1, this.Location.Y - 2);
        if (CHelper.CheckLocation(c))
            list.Add(c);

        return list;
    }


  
}
