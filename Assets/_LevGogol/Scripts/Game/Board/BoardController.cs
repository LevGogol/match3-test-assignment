using LevGogol.Match3.Game.Model;
using UnityEngine;

public class BoardController : MonoBehaviour
{
    private Board _board;

    [SerializeField] private BoardView _boardView;
    
    public void MakeBoard(int width, int height)
    {
        _board = new Board(width, height);

        _boardView.Initialize(_board);
        
        for (int x = 0; x < _board.Tiles.Widht; x++)
        {
            for (int y = 0; y < _board.Tiles.Height; y++)
            {
                _boardView.Tiles[x, y].TouchDowned += _board.OnTileTouched;
            }
        }
    }
}