using UnityEngine;

public class CrystalView : MonoBehaviour
{
    public void MoveTo(TileView tile)
    {
        transform.position = tile.transform.position;
        transform.parent = tile.transform;
    }
}
