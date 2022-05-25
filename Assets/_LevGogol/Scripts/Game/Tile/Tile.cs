using System;
using UnityEngine;

namespace LevGogol.Match3.Game.Model
{
    public class Tile
    {
        private Vector2Int _boardPosition;

        public Vector2Int BoardPosition => _boardPosition;

        public event Action Selected;
        public event Action Deselected;
        public event Action Cleared;

        public Crystal Crystal { get; set; }

        public Tile(Vector2Int boardPosition)
        {
            _boardPosition = boardPosition;
        }

        public void Select()
        {
            Selected?.Invoke();
        }

        public void Deselect()
        {
            Deselected?.Invoke();
        }

        public void Clear()
        {
            Crystal = null;
            
            Cleared?.Invoke();
        }
    }
}