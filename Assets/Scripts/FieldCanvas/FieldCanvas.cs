using DG.Tweening;
using System;
using TMPro;
using UnityEngine;

public class FieldCanvas : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private TMP_Text _topicText;
    [SerializeField] private FieldViewContainer _fieldViewContainer;
    [SerializeField] private FieldAnswerContainer _answerContainer;

    private const float MinFadeValue = 0.5f;

    private void OnValidate() => _canvas ??= GetComponent<Canvas>();

    private void Awake()
    {
        _topicText.text = Constants.FieldGameTopicName;
        _topicText.DOFade(MinFadeValue, Constants.DefaultDuration).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
        _fieldViewContainer.gameObject.SetActive(true);
    }

    private void OnEnable()
    {
        _fieldViewContainer.Selected += OnFieldSelected;
        _answerContainer.Selected += OnAnswerSelected;
    }

    private void OnDisable()
    {
        _fieldViewContainer.Selected -= OnFieldSelected;
        _answerContainer.Selected -= OnAnswerSelected;
    }

    private void OnFieldSelected(FieldData data)
    {
        _fieldViewContainer.Hide();

        _answerContainer.Show();
        _answerContainer.SetData(data);
    }

    private void OnAnswerSelected(FieldData data, bool isCorrect)
    {
        _answerContainer.Hide();
        _fieldViewContainer.Show();
        _fieldViewContainer.ShowAnswerStatus(data, isCorrect);
    }
}