using System;

using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button), typeof(Image))]
public class LightItemView : MonoBehaviour
{
    public event Action<int> OnClick;

    [SerializeField] Color _onColor;
    [SerializeField] Color _offColor;

    private Image _image;
    private Button _button;
    private int _index;

    void Awake()
    {
        _image = GetComponent<Image>();
        _button = GetComponent<Button>();
    }

    void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClick);
    }

    void OnDisable()
    {
        _button.onClick.RemoveAllListeners();
    }

    public void Initialize(int index, bool turnedOn = false)
    {
        _index = index;
        SetState(turnedOn);
    }

    public void SetState(bool turnedOn)
    {
        _image.color = turnedOn ? _onColor : _offColor;
    }

    private void OnButtonClick()
    {
        OnClick?.Invoke(_index);
    }
}
