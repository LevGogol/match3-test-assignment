using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] private BoardController _boardController;
    [SerializeField] private int _width;
    [SerializeField] private int _height;

    private void OnEnable()
    {
        _boardController.MakeBoard(_width, _height);
    }
}