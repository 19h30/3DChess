using UnityEngine;
using System.Collections;

public class P_Queen : BasePiece {



    public override void BeSelected()
    {
        playSelectedSound();

        HuongDiTuong();
        HuongDiXe();
    }

    private void HuongDiTuong()
    {
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

    private void HuongDiXe()
    {
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
