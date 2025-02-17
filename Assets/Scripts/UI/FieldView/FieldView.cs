using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(ElementRotator))]
public class FieldView : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private TMP_Text _fieldNumberText;
    [SerializeField] private TMP_Text _filedDescriptionText;
    [SerializeField] private Image _viewImage;
    [SerializeField] private Image _answerStatusImage;
    [SerializeField] private Color _clickedColor;
    [SerializeField] private AnswerStatusData _answerStatusData;
    [SerializeField] private ElementRotator _rotator;

    private FieldData _fieldData;
    private Sequence _sequence;
    private SoundContainer _soundContainer;

    private bool _isWorking;
    private bool _isClicked;

    public event Action<FieldData> Clicked;

    private void OnValidate()
    {
        _rotator ??= GetComponent<ElementRotator>();
        _viewImage ??= GetComponent<Image>();
    }

    private void Start()
    {
        WorkToggle(true);

        SetDescription();
        InitializeSequence();
    }

    public void Restart()
    {
        Debug.Log("ÐÅÑÒÀÐÒ!");
    }

    private void SetDescription() => _filedDescriptionText.text = _fieldData.Description;

    private void InitializeSequence()
    {
        _sequence = DOTween.Sequence();
        _sequence.Pause();
        _sequence.Append(_viewImage.DOColor(_clickedColor, 0));
        _sequence.Append(_fieldNumberText.DOFade(0, Constants.DefaultDuration).OnComplete(OpenField));
        _sequence.Append(_filedDescriptionText.DOFade(1, Constants.DefaultDuration));
    }

    public void Initialize(string fieldNumber, FieldData fieldData, SoundContainer soundContainer)
    {
        _fieldNumberText.text = fieldNumber;
        _fieldData = fieldData;
        _soundContainer = soundContainer;
    }

    public void WorkToggle(bool isWork) => _isWorking = isWork;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_isWorking == false || _isClicked)
            return;

        Clicked?.Invoke(_fieldData);
        ShowAnimations();
        _isClicked = true;
    }

    private void OpenField()
    {
        _fieldNumberText.gameObject.SetActive(false);
        _rotator.ResetRotate();
    }

    public bool TrySetAnswerStatus(FieldData fieldData, AnswerStatus answerStatus)
    {
        if (_fieldData != fieldData)
            return false;

        ShowAnswerStatus(answerStatus);

        return true;
    }

    private void ShowAnimations()
    {
        _rotator.Rotate();
        _sequence.Play();
    }

    private void ShowAnswerStatus(AnswerStatus answerStatus)
    {
        _answerStatusImage.sprite = _answerStatusData.GetSprite(answerStatus);
        _answerStatusImage.color = _answerStatusData.GetColor(answerStatus);
        _soundContainer.Play(answerStatus);
    }
}