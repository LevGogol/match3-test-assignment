using UnityEngine;

namespace LevGogol.Match3.Game.Model
{
    public class Tiles
    {
        private int _width;
        private int _height;
        private Tile[,] _value;

        public int Widht => _width;
        public int Height => _height;
        public Tile[,] Value => _value;

        public Tiles(int width, int height)
        {
            _width = width;
            _height = height;
            
            _value = new Tile[width, height];

            for (int x = 0; x < _width; x++)
            {
                for (int y = 0; y < _height; y++)
                {
                    _value[x, y] = new Tile(new Vector2Int(x, y));
                }                
            }
        }

        public Tile Get(Vector2Int position)
        {
            return Value[position.x, position.y];
        }
    }
}