using System;
using LevGogol.Match3.Game.Model;
using UnityEngine;

public class TileView : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Color _selectColor;
    [SerializeField] private Color _deselectColor;
    
    private Tile _tile;
    private CrystalView[] _crystals;

    public CrystalView CrystalView { get; set; }

    public void Initialize(Tile tile, CrystalView[] crystals)
    {
        _tile = tile;
        _crystals = crystals;
        
        var crystalModel = _tile.Crystal;
        CrystalView = Instantiate(_crystals[(int) crystalModel.CrystalColor], transform.position, Quaternion.identity, transform);
    }

    private void Start()
    {
        _tile.Selected += OnSelected;
        _tile.Deselected += OnDeselected;
        _tile.Cleared += OnCleared;
    }

    public void Redraw()
    {
        var crystalModel = _tile.Crystal;
        CrystalView = Instantiate(_crystals[(int) crystalModel.CrystalColor], transform.position, Quaternion.identity, transform);
    }

    private void OnSelected()
    {
        _spriteRenderer.color = _selectColor;
    }

    private void OnDeselected()
    {
        _spriteRenderer.color = _deselectColor;
    }

    private void OnCleared()
    {
        Destroy(CrystalView.gameObject);
        CrystalView = null;
    }

    private void OnDestroy()
    {
        _tile.Selected -= OnSelected;
        _tile.Deselected -= OnDeselected;
    }

    public event Action<Vector2Int> TouchDowned;
    
    private void OnMouseDown()
    {
        TouchDowned?.Invoke(_tile.BoardPosition);
    }
}