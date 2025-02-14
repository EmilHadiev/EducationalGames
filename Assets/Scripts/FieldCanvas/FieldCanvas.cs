using DG.Tweening;
using TMPro;
using UnityEngine;

public class FieldCanvas : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private TMP_Text _topicText;

    private const float MinFadeValue = 0.5f;

    private void OnValidate() => _canvas ??= GetComponent<Canvas>();

    private void Awake()
    {
        _topicText.text = Constants.FieldGameTopicName;
        _topicText.DOFade(MinFadeValue, Constants.DefaultDuration).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
    }
}