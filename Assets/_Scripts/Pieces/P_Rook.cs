using UnityEngine;
using System.Collections;

public class P_Rook : BasePiece
{

    /// <summary>
    /// Tìm các ô có khả năng di chuyển:
    /// 1. Trống
    /// 2. Quân địch
    /// 3. Chạy ngang dọc cho đến khi bị cản
    /// </summary>
    public override void BeSelected()
    {
        playSelectedSound();

        //Di chuyển ngang : Tọa độ y giữ nguyên
        for (int x = this.Location.X + 1; x < 8; x++)
        {
            //Nếu trống
            Cell c = ChessBoard.Current.Cells[x][this.Location.Y];
            if (c.CurrentPiece == null)
            {
                _targetedCells.Add(c);
            }
            else
            {
                if (c.CurrentPiece.Player != BaseGameCTL.Current.CurrentPlayer)
                    _targetedCells.Add(c);
                break;
            }
        }

        for (int x = this.Location.X - 1; x >= 0; x--)
        {
            //Nếu trống
            Cell c = ChessBoard.Current.Cells[x][this.Location.Y];
            if (c.CurrentPiece == null)
            {
                _targetedCells.Add(c);
            }
            else
            {
                if (c.CurrentPiece.Player != BaseGameCTL.Current.CurrentPlayer)
                    _targetedCells.Add(c);
                break;
            }
        }

        //Di chuyển dọc : Tọa độ x giữ nguyên
        for (int y = this.Location.Y + 1; y < 8; y++)
        {
            //Nếu trống
            Cell c = ChessBoard.Current.Cells[this.Location.X][y];
            if (c.CurrentPiece == null)
            {
                _targetedCells.Add(c);
            }
            else
            {
                if (c.CurrentPiece.Player != BaseGameCTL.Current.CurrentPlayer)
                    _targetedCells.Add(c);
                break;
            }
        }

        for (int y = this.Location.Y - 1; y >= 0; y--)
        {
            //Nếu trống
            Cell c = ChessBoard.Current.Cells[this.Location.X][y];
            if (c.CurrentPiece == null)
            {
                _targetedCells.Add(c);
            }
            else
            {
                if (c.CurrentPiece.Player != BaseGameCTL.Current.CurrentPlayer)
                    _targetedCells.Add(c);
                break;
            }
        }


        foreach (var item in _targetedCells)
            item.SetCellState(ECellState.TARGETED);
    }


 
}
