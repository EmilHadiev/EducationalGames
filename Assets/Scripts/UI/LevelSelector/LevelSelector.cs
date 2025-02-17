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
    private IStateSwitcher _switcher;

    public event Action<LevelSelectorData> Clicked;

    private void OnValidate()
    {
        _backgroundImage ??= GetComponent<Image>();
        _descriptionText ??= GetComponentInChildren<TMP_Text>();
        _levelImage ??= GetComponentInChildren<Image>();
    }

    public void Initialize(LevelSelectorData data, IStateSwitcher switcher)
    {
        _data = data;
        _levelImage.sprite = _data.Sprite;
        _descriptionText.text = _data.Name;
        _switcher = switcher;
    }

    public void BackgroundToggle(bool isOn) => _backgroundImage.enabled = isOn;

    public void OnPointerClick(PointerEventData eventData) => Click();

    private void Click()
    {
        Clicked?.Invoke(_data);
        SetLevel();
        BackgroundToggle(true);
    }

    private void SetLevel()
    {
        switch (_data.LevelType)
        {
            case LevelSelectorType.OpenField:
                _switcher.Switch<FieldGameMediator>();
                break;
            case LevelSelectorType.ChaseMaze:
                break;
            case LevelSelectorType.FlashCard:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(_data.LevelType));
        }
    }
}