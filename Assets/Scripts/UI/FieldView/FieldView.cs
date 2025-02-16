using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

public class FieldView : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private TMP_Text _fieldNumberText;
    [SerializeField] private TMP_Text _filedDescriptionText;
    [SerializeField] private Image _viewImage;
    [SerializeField] private Image _answerStatusImage;
    [SerializeField] private Color _clickedColor;
    [SerializeField] private AnswerStatusData _answerStatusData;
    [SerializeField] private Vector3 _rotationPosition = new Vector3(0, 180, 0);

    private readonly Vector3 _defaultRotation = new Vector3(0, 0, 0);

    private FieldData _fieldData;
    private Sequence _sequence;
    private SoundContainer _container;

    private bool _isWorking;
    private bool _isClicked;

    public event Action<FieldData> Clicked;

    private void OnValidate() => _viewImage ??= GetComponent<Image>();

    private void Start()
    {
        WorkToggle(true);

        SetDescription();
        InitializeSequence();
    }

    private void SetDescription() => _filedDescriptionText.text = _fieldData.Description;

    private void InitializeSequence()
    {
        _sequence = DOTween.Sequence();
        _sequence.Pause();
        _sequence.Append(transform.DORotate(_rotationPosition, Constants.DefaultDuration).SetEase(Ease.Linear));
        _sequence.Append(_viewImage.DOColor(_clickedColor, 0));
        _sequence.Append(_fieldNumberText.DOFade(0, Constants.DefaultDuration).OnComplete(OpenField));
        _sequence.Append(_filedDescriptionText.DOFade(1, Constants.DefaultDuration));
    }

    public void Initialize(string fieldNumber, FieldData fieldData, SoundContainer soundContainer)
    {
        _fieldNumberText.text = fieldNumber;
        _fieldData = fieldData;
        _container = soundContainer;
    }

    public void WorkToggle(bool isWork) => _isWorking = isWork;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_isWorking == false || _isClicked)
            return;

        Clicked?.Invoke(_fieldData);
        _sequence.Play();
        _isClicked = true;
    }

    private void OpenField()
    {
        _fieldNumberText.gameObject.SetActive(false);
        transform.DORotate(_defaultRotation, 0);
    }

    public bool TrySetAnswerStatus(FieldData fieldData, AnswerStatus answerStatus)
    {
        if (_fieldData != fieldData)
            return false;

        _answerStatusImage.sprite = _answerStatusData.GetSprite(answerStatus);
        _answerStatusImage.color = _answerStatusData.GetColor(answerStatus);

        _container.Play(answerStatus);

        return true;
    }
}