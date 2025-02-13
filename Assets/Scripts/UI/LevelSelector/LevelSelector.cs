using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image _backgroundImage;
    [SerializeField] private TMP_Text _descriptionText;
    [SerializeField] private Image _levelImage;

    private LevelSelectorData _data;

    public event Action<LevelSelectorData> Clicked;

    private void OnValidate()
    {
        _backgroundImage ??= GetComponent<Image>();
        _descriptionText ??= GetComponentInChildren<TMP_Text>();
        _levelImage ??= GetComponentInChildren<Image>();
    }

    private void Start()
    {
        Click();
    }

    public void Initialize(LevelSelectorData data)
    {
        _data = data;
        _levelImage.sprite = _data.Sprite;
        _descriptionText.text = _data.Name;
    }

    public void BackgroundToggle(bool isOn) => _backgroundImage.enabled = isOn;

    public void OnPointerClick(PointerEventData eventData) => Click();

    private void Click()
    {
        Clicked?.Invoke(_data);
        BackgroundToggle(true);
    }
}