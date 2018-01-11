using UnityEngine;
using System.Collections;

public class P_Pawn : BasePiece
{
    private bool isFirstMoved = true;

    public override void Move(Cell targetedCell)
    {
        isFirstMoved = false;
        base.Move(targetedCell);
    }

    public override void BeSelected()
    {
        playSelectedSound();

        switch (this._player)
        {
            case EPlayer.BLACK:
                BeSelected_Black();
                break;
            case EPlayer.WHITE:
                BeSelected_White();
                break;
        }
    }

    private void BeSelected_White()
    {
        //Hiển thị các nước đi có thể
        if (isFirstMoved)
        {
            //2. Có khả năng đi 2 bước về phía trước
            if (ChessBoard.Current.Cells[Location.X][Location.Y + 2].CurrentPiece == null
                && ChessBoard.Current.Cells[Location.X][Location.Y + 1].CurrentPiece == null
                )
                _targetedCells.Add(ChessBoard.Current.Cells[Location.X][Location.Y + 2]);
        }

        //1. Có khả năng đi 1 bước về phía trước
        if (ChessBoard.Current.Cells[Location.X][Location.Y + 1].CurrentPiece == null)
            _targetedCells.Add(ChessBoard.Current.Cells[Location.X][Location.Y + 1]);

        //3. Xác định 2 ô chéo phía trước có quân ăn hay không 
        //ăn: nếu như là quân địch, tức là quân đen
        if (Location.X > 0 && ChessBoard.Current.Cells[Location.X - 1][Location.Y + 1].CurrentPiece != null && ChessBoard.Current.Cells[Location.X - 1][Location.Y + 1].CurrentPiece.Player != BaseGameCTL.Current.CurrentPlayer)
            _targetedCells.Add(ChessBoard.Current.Cells[Location.X - 1][Location.Y + 1]);

        if (Location.X < 7 && ChessBoard.Current.Cells[Location.X + 1][Location.Y + 1].CurrentPiece != null && ChessBoard.Current.Cells[Location.X + 1][Location.Y + 1].CurrentPiece.Player != BaseGameCTL.Current.CurrentPlayer)
            _targetedCells.Add(ChessBoard.Current.Cells[Location.X + 1][Location.Y + 1]);

        //4. Xác định trường hợp bắt tốt qua đường

        foreach (var item in _targetedCells)
            item.SetCellState(ECellState.TARGETED);
    }

    private void BeSelected_Black()
    {
        //Hiển thị các nước đi có thể
        if (isFirstMoved)
        {
            //2. Có khả năng đi 2 bước về phía trước
            if (ChessBoard.Current.Cells[Location.X][Location.Y - 2].CurrentPiece == null
                && ChessBoard.Current.Cells[Location.X][Location.Y - 1].CurrentPiece == null
                )
                _targetedCells.Add(ChessBoard.Current.Cells[Location.X][Location.Y - 2]);
        }

        //1. Có khả năng đi 1 bước về phía trước
        if (ChessBoard.Current.Cells[Location.X][Location.Y - 1].CurrentPiece == null)
            _targetedCells.Add(ChessBoard.Current.Cells[Location.X][Location.Y - 1]);

        //3. Xác định 2 ô chéo phía trước có quân ăn hay không 
        //ăn: nếu như là quân địch, tức là quân đen
        if (Location.X > 0 && ChessBoard.Current.Cells[Location.X - 1][Location.Y - 1].CurrentPiece != null 
            && ChessBoard.Current.Cells[Location.X - 1][Location.Y - 1].CurrentPiece.Player != BaseGameCTL.Current.CurrentPlayer)
            _targetedCells.Add(ChessBoard.Current.Cells[Location.X - 1][Location.Y - 1]);

        if (Location.X < 7 && ChessBoard.Current.Cells[Location.X + 1][Location.Y - 1].CurrentPiece != null 
            && ChessBoard.Current.Cells[Location.X + 1][Location.Y - 1].CurrentPiece.Player != BaseGameCTL.Current.CurrentPlayer)
            _targetedCells.Add(ChessBoard.Current.Cells[Location.X + 1][Location.Y - 1]);
        

        //4. Xác định trường hợp bắt tốt qua đường
        foreach (var item in _targetedCells)
            item.SetCellState(ECellState.TARGETED);
    }


}
