using DG.Tweening;
using TMPro;
using UnityEngine;

public class TextFadeChanger : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    private const float MinFadeValue = 0.5f;

    private void OnValidate()
    {
        _text ??= GetComponent<TMP_Text>();
    }

    private void Start() => _text.DOFade(MinFadeValue, Constants.DefaultDuration).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
}