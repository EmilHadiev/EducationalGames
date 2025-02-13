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

    public event Action Clicked;

    private void OnValidate()
    {
        _backgroundImage ??= GetComponent<Image>();
        _descriptionText ??= GetComponentInChildren<TMP_Text>();
        _levelImage ??= GetComponentInChildren<Image>();
    }

    public void Initialize(Image levelImage, TMP_Text descriptionText)
    {
        _levelImage = levelImage;
        _descriptionText = descriptionText;
    }

    public void BackgroundToggle(bool isOn) => _backgroundImage.enabled = isOn;

    public void OnPointerClick(PointerEventData eventData)
    {
        Clicked?.Invoke();
        BackgroundToggle(true);
    }
}