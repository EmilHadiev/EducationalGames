using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FieldView : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private TMP_Text _fieldNumberText;
    [SerializeField] private TMP_Text _filedDescriptionText;
    [SerializeField] private Image _viewImage;
    [SerializeField] private Color _clickedColor;
    [SerializeField] private Vector3 _rotationPosition = new Vector3(0, 180, 0);

    private readonly Vector3 _defaultRotation = new Vector3(0, 0, 0);

    private FieldData _fieldData;
    private Sequence _sequence;

    private bool _isOpen;
    private bool _isWorking;

    public event Action<FieldData> Clicked;

    private void OnValidate() => _viewImage ??= GetComponent<Image>();

    private void Start()
    {
        StartWork();
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

    public void Initialize(string fieldNumber, FieldData fieldData)
    {
        _fieldNumberText.text = fieldNumber;
        _fieldData = fieldData;
    }

    public void StartWork() => _isWorking = true;

    public void StopWork() => _isWorking = false;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_isWorking == false)
            return;

        if (_isOpen)
            return;

        Clicked?.Invoke(_fieldData);
        _sequence.Play();
    }

    private void OpenField()
    {
        _fieldNumberText.gameObject.SetActive(false);
        transform.DORotate(_defaultRotation, 0);
    }
}
