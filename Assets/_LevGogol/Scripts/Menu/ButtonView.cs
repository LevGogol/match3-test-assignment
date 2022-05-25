using System;
using UnityEngine;
using UnityEngine.UI;

public class ButtonView : MonoBehaviour
{
    [SerializeField] private Button _button;

    public event Action ButtonCilcked;

    private void OnEnable()
    {
        _button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        ButtonCilcked.Invoke();
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnClick);
    }
}
