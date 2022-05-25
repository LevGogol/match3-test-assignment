using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(ButtonView))]
public class LoadSceneButtonController : MonoBehaviour
{
    [SerializeField] private ButtonView _buttonView;
    [SerializeField] private SceneAsset _scene;

    private LoadSceneButtonModel _loadSceneButtonModel;

    private void Start()
    {
        _loadSceneButtonModel = new LoadSceneButtonModel(_scene.name);
        _buttonView.ButtonCilcked += OnClick;
    }

    private void OnDestroy()
    {
        _buttonView.ButtonCilcked -= OnClick;
    }

    private void OnClick()
    {
        _loadSceneButtonModel.LoadNextLevel();
    }
}
