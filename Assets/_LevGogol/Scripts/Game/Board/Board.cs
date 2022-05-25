using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace LevGogol.Match3.Game.Model
{
    public class Board
    {
        private Tiles _tiles;
        private Tile _selectTile;

        public event Action<Vector2Int, Vector2Int> Swiped;
        public event Action<Vector2Int> DownShifted;
        public event Action<Vector2Int> CrystalSpawned;

        public Tiles Tiles => _tiles;

        public Board(int width, int height)
        {
            _tiles = new Tiles(width, height);

            FillEmptyTiles();
        }

        public void FillEmptyTiles()
        {
            for (int x = 0; x < _tiles.Widht; x++)
            {
                for (int y = 0; y < _tiles.Height; y++)
                {
                    if (_tiles.Get(new Vector2Int(x, y)).Crystal == null)
                    {
                        SpawnCrystal(x, y);
                        CrystalSpawned?.Invoke(new Vector2Int(x, y));
                    }
                }
            }
        }

        private void SpawnCrystal(int x, int y)
        {
            _tiles.Get(new Vector2Int(x, y)).Crystal =
                new Crystal((CrystalColor) Random.Range(0, Enum.GetValues(typeof(CrystalColor)).Length));
        }

        public void OnTileTouched(Vector2Int position)
        {
            if (_selectTile == null)
            {
                SelectTile(position);
            }
            else
            {
                if (IsTileNeighbour(_selectTile.BoardPosition, position))
                {
                    TrySwipeAndClear(position);
                }

                DeselectTile();
            }
        }

        private void SelectTile(Vector2Int position)
        {
            _selectTile = _tiles.Get(position);
            _selectTile.Select();
        }

        private void DeselectTile()
        {
            _selectTile.Deselect();
            _selectTile = null;
        }

        private bool IsTileNeighbour(Vector2Int position1, Vector2Int position2)
        {
            bool isSameX = position1.x == position2.x;
            bool isSameY = position1.y == position2.y;

            bool isHorizontalNeighbour = Mathf.Abs(position1.x - position2.x) == 1 && isSameY;
            bool isVerticalNeighbour = Mathf.Abs(position1.y - position2.y) == 1 && isSameX;

            return isHorizontalNeighbour ^ isVerticalNeighbour;
        }

        private void TrySwipeAndClear(Vector2Int position)
        {
            SwipeTileContent(_selectTile, _tiles.Get(position));

            while (TryClearTiles())
            {
                while (TryDownShift())
                {
                }

                FillEmptyTiles();
            }
        }

        private bool TryClearTiles()
        {
            HashSet<Tile> toClear = new HashSet<Tile>();

            PrepareHorizontal(toClear);
            PrepareVertical(toClear);

            foreach (var tile in toClear)
            {
                tile.Clear();
            }

            return toClear.Count > 0;
        }

        private bool TryDownShift()
        {
            var result = false;
            for (int x = 0; x < _tiles.Widht; x++)
            {
                for (int y = 0; y < _tiles.Height - 1; y++)
                {
                    var downTile = _tiles.Get(new Vector2Int(x, y));
                    var upTile = _tiles.Get(new Vector2Int(x, y + 1));
                    if (downTile.Crystal == null && upTile.Crystal != null) //TODO null is bad
                    {
                        downTile.Crystal = upTile.Crystal;
                        upTile.Crystal = null;
                        DownShifted?.Invoke(upTile.BoardPosition);

                        result = true;
                    }
                }
            }

            return result;
        }

        private void PrepareHorizontal(HashSet<Tile> toClear)
        {
            for (int y = 0; y < _tiles.Height; y++)
            {
                for (int x = 0; x < _tiles.Widht - 2; x++)
                {
                    var tile1 = _tiles.Get(new Vector2Int(x, y));
                    var tile2 = _tiles.Get(new Vector2Int(x + 1, y));
                    var tile3 = _tiles.Get(new Vector2Int(x + 2, y));

                    if (tile1.Crystal != null && tile2.Crystal != null && tile3.Crystal != null) //TODO null is bad
                    {
                        var isMatch1and2 = tile1.Crystal.CrystalColor == tile2.Crystal.CrystalColor;
                        var isMatch1and3 = tile1.Crystal.CrystalColor == tile3.Crystal.CrystalColor;
                        if (isMatch1and2 && isMatch1and3)
                        {
                            toClear.Add(tile1);
                            toClear.Add(tile2);
                            toClear.Add(tile3);
                        }
                    }
                }
            }
        }

        private void PrepareVertical(HashSet<Tile> toClear)
        {
            for (int x = 0; x < _tiles.Widht; x++)
            {
                for (int y = 0; y < _tiles.Height - 2; y++)
                {
                    var tile1 = _tiles.Get(new Vector2Int(x, y));
                    var tile2 = _tiles.Get(new Vector2Int(x, y + 1));
                    var tile3 = _tiles.Get(new Vector2Int(x, y + 2));

                    if (tile1.Crystal != null && tile2.Crystal != null && tile3.Crystal != null) //TODO null is bad
                    {
                        var isMatch1and2 = tile1.Crystal.CrystalColor == tile2.Crystal.CrystalColor;
                        var isMatch1and3 = tile1.Crystal.CrystalColor == tile3.Crystal.CrystalColor;
                        if (isMatch1and2 && isMatch1and3)
                        {
                            toClear.Add(tile1);
                            toClear.Add(tile2);
                            toClear.Add(tile3);
                        }
                    }
                }
            }
        }

        private void SwipeTileContent(Tile selectTile, Tile target)
        {
            var temporaryContent = selectTile.Crystal;
            selectTile.Crystal = target.Crystal;
            target.Crystal = temporaryContent;

            Swiped?.Invoke(selectTile.BoardPosition, target.BoardPosition);
        }
    }
}