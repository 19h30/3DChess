using UnityEngine;
using System.Collections;

public class P_Bishop : BasePiece
{


    public override void BeSelected()
    {
        playSelectedSound();
        ///Thực hiện kiểm tra 4 đường chéo, cho đến khi gặp điểm dừng
        //1. Phải trên : x++ y++
        CLocation c;
        c = this.Location;
        while (true)
        {
            c = new CLocation(c.X + 1, c.Y + 1);
            if (CHelper.CheckLocation(c))
            {
                Cell cell = ChessBoard.Current.Cells[c.X][c.Y];
                if (cell.CurrentPiece != null)
                {
                    if (cell.CurrentPiece.Player != BaseGameCTL.Current.CurrentPlayer)
                        _targetedCells.Add(cell);
                    break;
                }
                _targetedCells.Add(cell);
            }
            else
            {
                break;
            }
        }

        //2. Phải dưới : x++ y--
        c = this.Location;
        while (true)
        {
            c = new CLocation(c.X + 1, c.Y - 1);
            if (CHelper.CheckLocation(c))
            {
                Cell cell = ChessBoard.Current.Cells[c.X][c.Y];
                if (cell.CurrentPiece != null)
                {
                    if (cell.CurrentPiece.Player != BaseGameCTL.Current.CurrentPlayer)
                        _targetedCells.Add(cell);
                    break;
                }
                _targetedCells.Add(cell);
            }
            else
            {
                break;
            }
        }


        //3. Trái trên : x-- y++
        c = this.Location;
        while (true)
        {
            c = new CLocation(c.X - 1, c.Y + 1);
            if (CHelper.CheckLocation(c))
            {
                Cell cell = ChessBoard.Current.Cells[c.X][c.Y];
                if (cell.CurrentPiece != null)
                {
                    if (cell.CurrentPiece.Player != BaseGameCTL.Current.CurrentPlayer)
                        _targetedCells.Add(cell);
                    break;
                }
                _targetedCells.Add(cell);
            }
            else
            {
                break;
            }
        }


        //4. Trái dưới : x-- y--
        c = this.Location;
        while (true)
        {
            c = new CLocation(c.X - 1, c.Y - 1);
            if (CHelper.CheckLocation(c))
            {
                Cell cell = ChessBoard.Current.Cells[c.X][c.Y];
                if (cell.CurrentPiece != null)
                {
                    if (cell.CurrentPiece.Player != BaseGameCTL.Current.CurrentPlayer)
                        _targetedCells.Add(cell);
                    break;
                }
                _targetedCells.Add(cell);
            }
            else
            {
                break;
            }
        }



        foreach (var item in _targetedCells)
            item.SetCellState(ECellState.TARGETED);
    }


}
