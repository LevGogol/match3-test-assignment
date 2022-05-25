using LevGogol.Match3.Game.Model;
using UnityEngine;

public class BoardView : MonoBehaviour
{
    [SerializeField] private TileView _tilePrefab;
    [SerializeField] private CrystalView[] _crystals;
    
    private Board _board;
    private TileView[,] _tiles;

    public TileView[,] Tiles => _tiles;

    public void Initialize(Board board)
    {
        _board = board;
        DrawBoard(_board.Tiles);
    }

    private void Start()
    {
        _board.Swiped += OnSwiped;
        _board.DownShifted += OnDownShifted;
        _board.CrystalSpawned += OnCrystalSpawned;
    }

    private void DrawBoard(Tiles boardTiles)
    {
        _tiles = new TileView[boardTiles.Widht, boardTiles.Height];
        
        for (int x = 0; x < boardTiles.Widht; x++)
        {
            for (int y = 0; y < boardTiles.Height; y++)
            {
                var xPosition = RemapToZeroInCenter(x, boardTiles.Widht - 1);
                var yPosition = RemapToZeroInCenter(y, boardTiles.Height - 1);
                var position = new Vector3(xPosition, yPosition, 0);
                var tileView = Instantiate(_tilePrefab, position, Quaternion.identity, transform);
                _tiles[x, y] = tileView;
                tileView.Initialize(_board.Tiles.Get(new Vector2Int(x, y)), _crystals);
            }
        }
    }

    private void OnSwiped(Vector2Int selected, Vector2Int target)
    {
        _tiles[selected.x, selected.y].CrystalView.MoveTo(_tiles[target.x, target.y]);
        _tiles[target.x, target.y].CrystalView.MoveTo(_tiles[selected.x, selected.y]);
        
        var temporary = _tiles[selected.x, selected.y].CrystalView;
        _tiles[selected.x, selected.y].CrystalView = _tiles[target.x, target.y].CrystalView;
        _tiles[target.x, target.y].CrystalView = temporary;
    }

    private void OnDownShifted(Vector2Int tilePosition)
    {
        var upTile = _tiles[tilePosition.x, tilePosition.y];
        var downTile = _tiles[tilePosition.x, tilePosition.y - 1];
        
        upTile.CrystalView.MoveTo(downTile);
        downTile.CrystalView = upTile.CrystalView;
        upTile.CrystalView = null;
    }

    private void OnCrystalSpawned(Vector2Int position)
    {
        _tiles[position.x, position.y].Redraw();
    }

    private float RemapToZeroInCenter(float current, float max)
    {
        return current - max / 2f;
    }

    private void OnDestroy()
    {
        _board.Swiped -= OnSwiped;
        _board.DownShifted -= OnDownShifted;
    }
}